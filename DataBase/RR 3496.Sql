alter table "TblBrowsers" add "PropertyJson" json;
alter table "TblBrowsers" add "IsMobile" boolean;
update "TblBrowsers" set "IsMobile" = 'f';
alter table "TblSharedTestData" add "BrowserId" bigint;
alter table "TblTestData" add "BrowserId" bigint;

-- Function: procsearchreportv3(timestamp without time zone, timestamp without time zone, bigint, bigint, bigint, text, text, integer, bigint, bigint, integer, boolean, text)

-- DROP FUNCTION procsearchreportv3(timestamp without time zone, timestamp without time zone, bigint, bigint, bigint, text, text, integer, bigint, bigint, integer, boolean, text);

CREATE OR REPLACE FUNCTION procsearchreportv3(
    IN startdate timestamp without time zone,
    IN enddate timestamp without time zone,
    IN websiteid bigint,
    IN suiteid bigint,
    IN testid bigint,
    IN osname text,
    IN browser text,
    IN teststatus integer,
    IN userid bigint,
    IN startat bigint,
    IN pagesize integer,
    IN extradata boolean,
    IN executiongroup text)
  RETURNS TABLE("Count" bigint, "CountPassed" bigint, "CountFailed" bigint, "Status" integer, "Id" bigint, "TestQueueId" bigint, "ExecutionGroup" character varying, "SuiteId" integer, "TestId" integer, "SuiteName" character varying, "TestName" character varying, "Passed" boolean, os text, browsername text, browserversion text, finishtime text, "FinishedAt" text, screenshotfile text, "LastStepExecuted" text, "QueuedBy" text, "VariableStateContainer" text, "LogContainer" text) AS
$BODY$
Declare
	CountPassed bigint;
	CountFailed bigint;
BEGIN
	if(pagesize <= 0 )
	Then 
		pagesize = 5000;
	End if;
	
	Return Query
	SELECT 
		count(*) OVER() AS full_count,
		sum(case when coalesce("RD"."Status", "TQ"."Status") = 8 then 1 else 0 end) OVER() AS CountPassed,
		sum(case when coalesce("RD"."Status", "TQ"."Status") = 9 then 1 else 0 end) OVER() AS CountFailed,
		coalesce("RD"."Status", "TQ"."Status") as "Status",
		cast(coalesce("RD"."Id", 0) as bigint),
		cast("TQ"."Id" as bigint) as TestQueueId,
		coalesce("RD"."ExecutionGroup", "TQ"."GroupName"),
		"TQ"."SuiteId",
		"TQ"."TestId", 
		"S"."Name" as "SuiteName", 
		"T"."TestName" as "TestName",
		cast("RD"."Value"::json->>'Passed' as boolean),
		coalesce("RD"."Value"::json->>'Os', "BR"."Platform"),
		coalesce("RD"."Value"::json->>'BrowserName', "BR"."ConfigName"),
		coalesce("RD"."Value"::json->>'BrowserVersion', "BR"."Version"),
		"RD"."Value"::json->>'FinishTime',
		"RD"."Value"::json->>'FinishedAt',
		"RD"."Value"::json->>'ScreenShotFile',
		"RD"."Value"::json->>'LastStepExecuted',
		trim("U"."FirstName" || ' ' || "U"."LastName"),
		case when extraData = '1' then "RD"."Value"::json->>'VariableStateContainer' else '' end as VariableStateContainer,
		case when extraData = '1' then "RD"."Value"::json->>'LogContainer' else '' end as LogContainer
		From "TblTestQueue" as "TQ"
		LEFT JOIN "TblUsers" as "U" ON ("TQ"."CreatedBy" = "U"."Id")
		LEFT JOIN "TblTest" as "T" ON ("TQ"."TestId" = "T"."Id")
		LEFT JOIN "TblWebsite" as "W" On "T"."WebsiteId" = "W"."Id"
		LEFT JOIN "TblSuite" as "S" ON ("TQ"."SuiteId" = "S"."Id")
		LEFT JOIN "TblSchedulerHistory" as "SH" On "SH"."GroupName" = "TQ"."GroupName" and "SH"."SchedulerId" = "TQ"."SchedulerId"
		LEFT Join LATERAL json_array_elements(
		case when coalesce("TQ"."SettingsJson"::json->>'Browsers', '1') = '1' then 
			case when coalesce("SH"."SettingsJson"::json->>'Browsers', '1') = '1' then 
				"W"."SettingsJson"::json->'Browsers'
			else
				"SH"."SettingsJson"::json->'Browsers'
			end
		else 
			"TQ"."SettingsJson"::json->'Browsers'
		end
		) TQB(BrowserId) on true
		LEFT JOIN "TblBrowsers" as "BR" ON ("BR"."Id" = TQB.BrowserId::text::int)
		LEFT JOIN "TblReportData" as "RD" ON ("RD"."TestQueueId" = "TQ"."Id" 
						      and (("RD"."Value"::json->>'Os' = 'ANDROID' and "RD"."Value"::json->>'BrowserName' in ('chrome')) OR "RD"."Value"::json->>'BrowserName' in ("BR"."ConfigName"))
						      and 
						      case when "RD"."Value"::json->>'Os' = 'XP' Then 'WINDOWS' else "RD"."Value"::json->>'Os' end
						      in ("BR"."Platform"))
		
 		where (startDate Is Null OR enddate Is Null OR "TQ"."CreatedOn"::Date between startDate::Date AND enddate::Date) 
 		AND (executiongroup Is Null OR ("TQ"."GroupName" = executiongroup))
 		AND (suiteid = 0 or "TQ"."SuiteId" = suiteid)
 		AND (testid = 0 or "TQ"."TestId" = testid)
		AND (browser is null or length(browser) = 0 or coalesce("RD"."Value"::json->>'BrowserName', "BR"."ConfigName") = browser)
 		AND (osname is null or length(osname) = 0 or coalesce("RD"."Value"->>'Os', "BR"."Platform") = osname)
 		AND (userid = 0 or "U"."Id" = userid)
 		AND (teststatus = 0 or coalesce("RD"."Status", "TQ"."Status") = teststatus)
		AND (websiteid = 0 or "W"."Id" = websiteid)
		order by "TQ"."Id" desc
 		offset startat
		limit pagesize;
END;
$BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 5000;
ALTER FUNCTION procsearchreportv3(timestamp without time zone, timestamp without time zone, bigint, bigint, bigint, text, text, integer, bigint, bigint, integer, boolean, text)
  OWNER TO postgres;