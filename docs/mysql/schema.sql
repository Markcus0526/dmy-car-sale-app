-- =====================================================================
-- CarSaleMan (csm) — MySQL reference schema
--
-- GENERATED from CarSaleMan/CarSaleMan/CmsDB.xsd (the typed-DataSet schema),
-- which is the only schema description present in this repository.
--
-- !! THIS IS INFERRED, NOT AUTHORITATIVE !!
-- CmsDB.xsd cannot express: DEFAULT values, indexes, CHECK constraints,
-- triggers, computed columns, or the real collation. Reconcile against the
-- output of docs/mssql-export.sql (Phase 0) before applying to production.
--
-- All string columns are NVARCHAR in the source -> utf8mb4 throughout.
-- All money/rate columns are DECIMAL -- never map these to FLOAT/DOUBLE.
-- =====================================================================

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

CREATE DATABASE IF NOT EXISTS csm
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;
USE csm;

-- ---------------------------------------------------------------------
-- SECTION 1: base tables (15)
-- ---------------------------------------------------------------------

DROP TABLE IF EXISTS `tbl_userinfo`;
CREATE TABLE `tbl_userinfo` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `departmentcode` VARCHAR(50) NOT NULL,
  `departmentname` VARCHAR(100) NULL,
  `username` VARCHAR(50) NULL,
  `password` VARCHAR(50) NULL,
  PRIMARY KEY (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_cartype`;
CREATE TABLE `tbl_cartype` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `carseries` VARCHAR(100) NOT NULL,
  `carcode` VARCHAR(50) NOT NULL,
  `carname` VARCHAR(200) NOT NULL,
  `eop` VARCHAR(50) NOT NULL,
  `subsets` VARCHAR(50) NULL,
  `insidesetcode` VARCHAR(50) NULL,
  `insidesetname` VARCHAR(100) NULL,
  `inprice` DECIMAL(10,2) NOT NULL,
  `outprice` DECIMAL(10,2) NOT NULL,
  `otherprice1` DECIMAL(10,2) NOT NULL,
  `otherprice2` DECIMAL(10,2) NOT NULL,
  `otherprice3` DECIMAL(10,2) NOT NULL,
  `otherprice4` DECIMAL(10,2) NOT NULL,
  `propval` DECIMAL(10,2) NOT NULL,
  `profitval` DECIMAL(10,3) NOT NULL,
  `outstoreprice` DECIMAL(10,3) NOT NULL,
  `vinprefix` VARCHAR(50) NOT NULL,
  `enginenoprefix` VARCHAR(50) NOT NULL,
  `deleted` SMALLINT NOT NULL,
  PRIMARY KEY (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_dbbackup_log`;
CREATE TABLE `tbl_dbbackup_log` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `path` VARCHAR(200) NOT NULL,
  `backupdate` DATETIME NOT NULL,
  `username` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_env`;
CREATE TABLE `tbl_env` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(50) NOT NULL,
  `value` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_onroad`;
CREATE TABLE `tbl_onroad` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `billno` VARCHAR(50) NOT NULL,
  `billdate` DATETIME NOT NULL,
  `vin` VARCHAR(50) NOT NULL,
  `engineno` VARCHAR(50) NOT NULL,
  `cartypeid` INT NOT NULL,
  `cartype` VARCHAR(50) NOT NULL,
  `carname` VARCHAR(200) NULL,
  `colorcode` VARCHAR(50) NULL,
  `colorname` VARCHAR(100) NULL,
  `subsets` VARCHAR(50) NULL,
  `insidesetcode` VARCHAR(50) NULL,
  `insidesetname` VARCHAR(100) NULL,
  `carstate` VARCHAR(50) NULL,
  `property` VARCHAR(50) NULL,
  `inprice` DECIMAL(10,2) NULL,
  `inflag` SMALLINT NOT NULL,
  `inkind` SMALLINT NOT NULL,
  PRIMARY KEY (`uid`),
  KEY `idx_tbl_onroad_cartypeid` (`cartypeid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_permission`;
CREATE TABLE `tbl_permission` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `userinfoid` INT NOT NULL,
  `fieldname` VARCHAR(50) NOT NULL,
  `permission` VARCHAR(50) NULL,
  PRIMARY KEY (`uid`),
  KEY `idx_tbl_permission_userinfoid` (`userinfoid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_stats`;
CREATE TABLE `tbl_stats` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `statsdate` VARCHAR(50) NOT NULL,
  `companyname` VARCHAR(100) NOT NULL,
  `bonus1` DECIMAL(10,2) NOT NULL,
  `bonus2` DECIMAL(10,2) NOT NULL,
  `bonus3` DECIMAL(10,2) NOT NULL,
  `bonus4` DECIMAL(10,2) NOT NULL,
  `bonus5` DECIMAL(10,2) NOT NULL,
  `bonus6` DECIMAL(10,2) NOT NULL,
  `bonus7` DECIMAL(10,2) NOT NULL,
  `total` DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_basedata`;
CREATE TABLE `tbl_basedata` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `type` INT NOT NULL,
  `name` VARCHAR(50) NOT NULL,
  `keyname` VARCHAR(100) NULL,
  `value` VARCHAR(100) NULL,
  PRIMARY KEY (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_storein`;
CREATE TABLE `tbl_storein` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `batchno` VARCHAR(50) NOT NULL,
  `storeplace` VARCHAR(50) NOT NULL,
  `onroadid` INT NOT NULL,
  `indate` DATETIME NOT NULL,
  `inprice` DECIMAL(10,2) NOT NULL,
  `passno` VARCHAR(50) NOT NULL,
  `companyno` SMALLINT NOT NULL,
  `inpath` VARCHAR(100) NOT NULL,
  `intype` VARCHAR(100) NOT NULL,
  `incarpricekind` VARCHAR(50) NOT NULL,
  `factoryoutdate` DATETIME NOT NULL,
  `repairstate` VARCHAR(100) NULL,
  `reservestate` VARCHAR(100) NULL,
  `propval` DECIMAL(10,2) NOT NULL,
  `profitprop` DECIMAL(10,2) NOT NULL,
  `profitval` DECIMAL(10,3) NOT NULL,
  `specprofitval` DECIMAL(10,2) NOT NULL,
  `profitstate` VARCHAR(50) NOT NULL,
  `outstoreprice` DECIMAL(10,3) NOT NULL,
  `settlementname` VARCHAR(50) NOT NULL,
  `handlername` VARCHAR(50) NOT NULL,
  `remark` VARCHAR(500) NULL,
  `outflag` SMALLINT NULL,
  `changedate` DATETIME NOT NULL,
  PRIMARY KEY (`uid`),
  KEY `idx_tbl_storein_onroadid` (`onroadid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_storeout`;
CREATE TABLE `tbl_storeout` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `batchno` VARCHAR(50) NOT NULL,
  `onroadid` INT NOT NULL,
  `salecompany` VARCHAR(100) NOT NULL,
  `outbillno` VARCHAR(50) NOT NULL,
  `salekind` VARCHAR(50) NOT NULL,
  `settlementname` VARCHAR(50) NOT NULL,
  `handlername` VARCHAR(50) NOT NULL,
  `saleplace` VARCHAR(50) NOT NULL,
  `incarkind` VARCHAR(100) NOT NULL,
  `outdate` DATETIME NOT NULL,
  `customername` VARCHAR(50) NOT NULL,
  `customerphoneno` VARCHAR(50) NULL,
  `customerjobkind` VARCHAR(50) NOT NULL,
  `saleregion` VARCHAR(100) NOT NULL,
  `customeraddress` VARCHAR(100) NULL,
  `carno` VARCHAR(50) NOT NULL,
  `carspeckind` VARCHAR(50) NULL,
  `isreport` VARCHAR(50) NULL,
  `votecost` DECIMAL(10,2) NOT NULL,
  `inprice` DECIMAL(10,2) NULL,
  `otherprice1` DECIMAL(10,2) NULL,
  `otherprice2` DECIMAL(10,2) NULL,
  `otherprice3` DECIMAL(10,2) NULL,
  `otherprice4` DECIMAL(10,2) NULL,
  `interestprice` DECIMAL(10,2) NULL,
  `profitval` DECIMAL(10,2) NULL,
  `specprofitval` DECIMAL(10,2) NULL,
  `outprice` DECIMAL(10,2) NOT NULL,
  `pricediff` DECIMAL(10,2) NULL,
  `isbill` VARCHAR(50) NULL,
  `billoutdate` DATETIME NULL,
  `ispayment` VARCHAR(50) NULL,
  `paymentdate` DATETIME NULL,
  `issend` VARCHAR(50) NULL,
  `senddate` DATETIME NULL,
  `remark` VARCHAR(500) NULL,
  PRIMARY KEY (`uid`),
  KEY `idx_tbl_storeout_onroadid` (`onroadid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_quarterstats`;
CREATE TABLE `tbl_quarterstats` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `year` INT NOT NULL,
  `carseries` VARCHAR(50) NULL,
  `type` TINYINT UNSIGNED NULL,
  `total1` INT NULL,
  `total2` INT NULL,
  `total3` INT NULL,
  `total4` INT NULL,
  `m1` INT NULL,
  `m2` INT NULL,
  `m3` INT NULL,
  `m4` INT NULL,
  `m5` INT NULL,
  `m6` INT NULL,
  `m7` INT NULL,
  `m8` INT NULL,
  `m9` INT NULL,
  `m10` INT NULL,
  `m11` INT NULL,
  `m12` INT NULL,
  `remain1` INT NULL,
  `remain2` INT NULL,
  `remain3` INT NULL,
  `remain4` INT NULL,
  PRIMARY KEY (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_carcompany`;
CREATE TABLE `tbl_carcompany` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `companyno` SMALLINT NOT NULL,
  `companyname` VARCHAR(100) NOT NULL,
  `address` VARCHAR(100) NULL,
  `postno` VARCHAR(100) NULL,
  `telno` VARCHAR(100) NULL,
  `cartypes` VARCHAR(100) NULL,
  `email` VARCHAR(100) NULL,
  `managername` VARCHAR(100) NULL,
  `linkmanname` VARCHAR(100) NULL,
  `property` VARCHAR(100) NULL,
  `remark` VARCHAR(500) NULL,
  PRIMARY KEY (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_log`;
CREATE TABLE `tbl_log` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `logdate` DATETIME NOT NULL,
  `title` VARCHAR(50) NULL,
  `cont` VARCHAR(200) NULL,
  PRIMARY KEY (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_storechange`;
CREATE TABLE `tbl_storechange` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `changeid` INT NOT NULL,
  `batchno` VARCHAR(50) NOT NULL,
  `onroadid` INT NOT NULL,
  `storeplace` VARCHAR(100) NOT NULL,
  `actionkind` VARCHAR(50) NOT NULL,
  `actiondate` DATETIME NOT NULL,
  `actionpay` DECIMAL(10,2) NOT NULL,
  `settlementname` VARCHAR(50) NOT NULL,
  `handlername` VARCHAR(50) NOT NULL,
  `repairstate` VARCHAR(100) NULL,
  `reservestate` VARCHAR(100) NULL,
  `remark` VARCHAR(500) NULL,
  PRIMARY KEY (`uid`),
  KEY `idx_tbl_storechange_onroadid` (`onroadid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DROP TABLE IF EXISTS `tbl_fit`;
CREATE TABLE `tbl_fit` (
  `uid` INT NOT NULL AUTO_INCREMENT,
  `consumer` VARCHAR(50) NOT NULL,
  `seller` VARCHAR(50) NULL,
  `fitdate` DATETIME NULL,
  `fitprice` DECIMAL(10,2) NULL,
  `remark` VARCHAR(100) NULL,
  PRIMARY KEY (`uid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ---------------------------------------------------------------------
-- SECTION 2: foreign keys
--
-- Only 4 relationships are declared in CmsDB.xsd; tbl_storechange.onroadid
-- is implied by the vw_* joins but never declared. Validate the legacy data
-- for orphans BEFORE adding these -- years of unconstrained operation make
-- dangling references likely.
-- ---------------------------------------------------------------------

ALTER TABLE `tbl_permission`
  ADD CONSTRAINT `fk_tbl_permission_userinfoid`
  FOREIGN KEY (`userinfoid`) REFERENCES `tbl_userinfo` (`uid`);
ALTER TABLE `tbl_onroad`
  ADD CONSTRAINT `fk_tbl_onroad_cartypeid`
  FOREIGN KEY (`cartypeid`) REFERENCES `tbl_cartype` (`uid`);
ALTER TABLE `tbl_storein`
  ADD CONSTRAINT `fk_tbl_storein_onroadid`
  FOREIGN KEY (`onroadid`) REFERENCES `tbl_onroad` (`uid`);
ALTER TABLE `tbl_storeout`
  ADD CONSTRAINT `fk_tbl_storeout_onroadid`
  FOREIGN KEY (`onroadid`) REFERENCES `tbl_onroad` (`uid`);
ALTER TABLE `tbl_storechange`
  ADD CONSTRAINT `fk_tbl_storechange_onroadid`
  FOREIGN KEY (`onroadid`) REFERENCES `tbl_onroad` (`uid`);

SET FOREIGN_KEY_CHECKS = 1;

-- ---------------------------------------------------------------------
-- SECTION 3: views to translate (5)
--
-- Definitions are NOT in this repo -- dump them via docs/mssql-export.sql.
-- The exact column list each view must expose is recorded below; the
-- translated MySQL view must match it name-for-name, because the Go
-- repositories and the report code are written against these columns.
-- ---------------------------------------------------------------------

-- VIEW vw_department  (2 columns)
--   departmentcode, departmentname

-- VIEW vw_speccar  (53 columns)
--   uid, batchno, onroadid, salecompany, outbillno, salekind, settlementname, handlername, saleplace, incarkind, outdate, customername, customerphoneno, customerjobkind, saleregion, customeraddress, carno, carspeckind, isreport, votecost, inprice, otherprice1, otherprice2, otherprice3, otherprice4, interestprice, profitval, specprofitval, outprice, pricediff, isbill, billoutdate, ispayment, paymentdate, issend, senddate, remark, billno, billdate, vin, engineno, cartypeid, cartype, carname, colorcode, colorname, subsets, insidesetcode, insidesetname, carstate, property, inkind, inflag

-- VIEW vw_onroad  (19 columns)
--   uid, billno, billdate, vin, engineno, cartypeid, cartype, carname, colorcode, colorname, subsets, insidesetcode, insidesetname, carstate, property, inprice, inflag, inkind, carseries

-- VIEW vw_storein  (42 columns)
--   uid, batchno, storeplace, onroadid, indate, inprice, passno, companyno, inpath, intype, incarpricekind, factoryoutdate, repairstate, reservestate, propval, profitprop, profitval, specprofitval, profitstate, outstoreprice, settlementname, handlername, remark, outflag, billno, billdate, vin, engineno, cartypeid, cartype, carname, colorcode, subsets, colorname, insidesetcode, insidesetname, carstate, property, inflag, inkind, changedate, carseries

-- VIEW vw_storeout  (55 columns)
--   uid, onroadid, batchno, salecompany, outbillno, salekind, settlementname, handlername, saleplace, incarkind, outdate, customername, customerphoneno, customerjobkind, saleregion, customeraddress, carno, carspeckind, isreport, votecost, inprice, otherprice1, otherprice2, otherprice3, otherprice4, interestprice, profitval, outprice, pricediff, isbill, billoutdate, ispayment, paymentdate, issend, senddate, remark, billno, billdate, vin, cartypeid, engineno, cartype, carname, colorcode, colorname, subsets, insidesetcode, insidesetname, carstate, property, Expr1, inflag, inkind, specprofitval, carseries
--   NOTE: column `Expr1` is an unnamed computed expression in the
--         original view. Preserve the name during migration.

-- ---------------------------------------------------------------------
-- SECTION 4: stored procedures to translate (28)
--
-- These contain ALL report / statistics / chart logic. Bodies are NOT in
-- this repo (Database/ was deleted in commit 8f19f54) -- dump them via
-- docs/mssql-export.sql.
--
-- Recovered below: exact signature + exact result-column contract for each.
-- A translation is correct when it accepts these params and returns these
-- columns with identical values (see Phase 2.3 golden-file tests).
-- ---------------------------------------------------------------------

-- PROC stor_statis_onroad()
--   returns 27 columns:
--     uid INT
--     billno VARCHAR(50)
--     billdate DATETIME
--     vin VARCHAR(50)
--     engineno VARCHAR(50)
--     cartypeid INT
--     cartype VARCHAR(50)
--     carname VARCHAR(200) NULL
--     colorcode VARCHAR(50) NULL
--     colorname VARCHAR(100) NULL
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     insidesetname VARCHAR(100) NULL
--     carstate VARCHAR(50) NULL
--     property VARCHAR(50) NULL
--     inprice DECIMAL(10,2) NULL
--     inflag SMALLINT
--     inkind SMALLINT
--     nointerestdate DATETIME NULL
--     noextenddate DATETIME NULL
--     noallmoneydate DATETIME NULL
--     interestdates INT NULL
--     distnointerestdates INT NULL
--     distextenddates INT NULL
--     distallmoney INT NULL
--     interestrate DECIMAL(10,2) NULL
--     totalinterest DECIMAL(10,2) NULL

-- PROC stor_finance_store()
--   returns 18 columns:
--     vin VARCHAR(50)
--     cartype VARCHAR(50)
--     subsets VARCHAR(50) NULL
--     inprice DECIMAL(10,2) NULL
--     goodprice DECIMAL(10,2) NULL
--     goodremainprice DECIMAL(10,2) NULL
--     billdate DATETIME
--     nointerestdate DATETIME NULL
--     noextenddate DATETIME NULL
--     noallmoneydate DATETIME NULL
--     distnointerestdates INT NULL
--     interestdates INT NULL
--     distextenddates INT NULL
--     distallmoney INT NULL
--     interestrate DECIMAL(10,2) NULL
--     dateinterest DECIMAL(10,2) NULL
--     totalinterest DECIMAL(10,2) NULL
--     storeindates INT NULL

-- PROC stor_statis_storein()
--   returns 25 columns:
--     storeplace VARCHAR(50)
--     cartype VARCHAR(50)
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     vin VARCHAR(50)
--     engineno VARCHAR(50)
--     passno VARCHAR(50)
--     billdate DATETIME
--     indate DATETIME
--     changedate DATETIME
--     outstoreprice DECIMAL(10,2)
--     inprice DECIMAL(10,2)
--     intype VARCHAR(100)
--     inpath VARCHAR(100)
--     companyno SMALLINT
--     savedates INT NULL
--     nointerestdate DATETIME NULL
--     noextenddate DATETIME NULL
--     noallmoneydate DATETIME NULL
--     interestdates INT NULL
--     distnointerestdates INT NULL
--     distextenddates INT NULL
--     distallmoney INT NULL
--     interestrate DECIMAL(10,2) NULL
--     totalinterest DECIMAL(10,2) NULL

-- PROC stor_statis_storeout()
--   returns 47 columns:
--     cartype VARCHAR(50)
--     carname VARCHAR(200) NULL
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     insidesetname VARCHAR(100) NULL
--     vin VARCHAR(50)
--     engineno VARCHAR(50)
--     billdate DATETIME
--     storeplace VARCHAR(50)
--     passno VARCHAR(50)
--     uid INT
--     batchno VARCHAR(50)
--     onroadid INT
--     salecompany VARCHAR(100)
--     outbillno VARCHAR(50)
--     salekind VARCHAR(50)
--     settlementname VARCHAR(50)
--     handlername VARCHAR(50)
--     saleplace VARCHAR(50)
--     incarkind VARCHAR(100)
--     outdate DATETIME
--     customername VARCHAR(50)
--     customerphoneno VARCHAR(50) NULL
--     customerjobkind VARCHAR(50)
--     saleregion VARCHAR(100)
--     customeraddress VARCHAR(100) NULL
--     carno VARCHAR(50)
--     carspeckind VARCHAR(50) NULL
--     isreport VARCHAR(50) NULL
--     votecost DECIMAL(10,2)
--     inprice DECIMAL(10,2) NULL
--     otherprice1 DECIMAL(10,2) NULL
--     otherprice2 DECIMAL(10,2) NULL
--     otherprice3 DECIMAL(10,2) NULL
--     otherprice4 DECIMAL(10,2) NULL
--     interestprice DECIMAL(10,2) NULL
--     profitval DECIMAL(10,2) NULL
--     specprofitval DECIMAL(10,2) NULL
--     outprice DECIMAL(10,2)
--     pricediff DECIMAL(10,2) NULL
--     isbill VARCHAR(50) NULL
--     billoutdate DATETIME NULL
--     ispayment VARCHAR(50) NULL
--     paymentdate DATETIME NULL
--     issend VARCHAR(50) NULL
--     senddate DATETIME NULL
--     remark VARCHAR(500) NULL

-- PROC stor_storeout_region(@startdate DateTime, @enddate DateTime)
--   returns 14 columns:
--     saleregion VARCHAR(100)
--     cnt1 INT NULL
--     cnt2 INT NULL
--     cnt3 INT NULL
--     cnt4 INT NULL
--     cnt5 INT NULL
--     cnt6 INT NULL
--     cnt7 INT NULL
--     cnt8 INT NULL
--     cnt9 INT NULL
--     cnt10 INT NULL
--     cnt11 INT NULL
--     cnt12 INT NULL
--     cnttotal INT NULL

-- PROC stor_storeout_custsjob(@startdate DateTime, @enddate DateTime)
--   returns 14 columns:
--     customerjobkind VARCHAR(50)
--     cnt1 INT NULL
--     cnt2 INT NULL
--     cnt3 INT NULL
--     cnt4 INT NULL
--     cnt5 INT NULL
--     cnt6 INT NULL
--     cnt7 INT NULL
--     cnt8 INT NULL
--     cnt9 INT NULL
--     cnt10 INT NULL
--     cnt11 INT NULL
--     cnt12 INT NULL
--     cnttotal INT NULL

-- PROC stor_statis_cartypecolor(@startdate DateTime, @enddate DateTime)
--   returns 6 columns:
--     carseries VARCHAR(100)
--     carname VARCHAR(200) NULL
--     subsets VARCHAR(50) NULL
--     colorname VARCHAR(100) NULL
--     insidesetname VARCHAR(100) NULL
--     amount INT NULL

-- PROC stor_statis_handler(@startdate DateTime, @enddate DateTime)
--   returns 3 columns:
--     handlername VARCHAR(50)
--     carseries VARCHAR(100)
--     amount INT NULL

-- PROC stor_statis_remainamount()
--   returns 10 columns:
--     carseries VARCHAR(100) NULL
--     cartype VARCHAR(50) NULL
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     storecount INT NULL
--     storeamount DECIMAL(10,2) NULL
--     onroadcount INT NULL
--     onroadamount DECIMAL(10,2) NULL
--     storecountpercent DECIMAL(10,2) NULL
--     storeamountpercent DECIMAL(10,2) NULL

-- PROC stor_statis_salecartype(@startdate DateTime, @enddate DateTime)
--   returns 14 columns:
--     carseries VARCHAR(100)
--     cnt1 INT NULL
--     cnt2 INT NULL
--     cnt3 INT NULL
--     cnt4 INT NULL
--     cnt5 INT NULL
--     cnt6 INT NULL
--     cnt7 INT NULL
--     cnt8 INT NULL
--     cnt9 INT NULL
--     cnt10 INT NULL
--     cnt11 INT NULL
--     cnt12 INT NULL
--     cnttotal INT NULL

-- PROC stor_report_onroaddetail(@startdate DateTime, @enddate DateTime)
--   returns 11 columns:
--     carseries VARCHAR(100)
--     billno VARCHAR(50)
--     billdate DATETIME
--     cartype VARCHAR(50)
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     colorcode VARCHAR(50) NULL
--     vin VARCHAR(50)
--     inprice DECIMAL(10,2) NULL
--     inflag SMALLINT
--     carstate VARCHAR(50) NULL

-- PROC stor_report_storeouttotal()
--   returns 17 columns:
--     carseries VARCHAR(100)
--     billdate DATETIME
--     salecompany VARCHAR(100)
--     cartype VARCHAR(50)
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     colorcode VARCHAR(50) NULL
--     outbillno VARCHAR(50)
--     vin VARCHAR(50)
--     votecost DECIMAL(10,2)
--     otherprice3 DECIMAL(10,2) NULL
--     otherprice4 DECIMAL(10,2) NULL
--     interestprice DECIMAL(10,2) NULL
--     inprice DECIMAL(10,2) NULL
--     outprice DECIMAL(10,2)
--     profitval DECIMAL(10,2) NULL
--     pricediff DECIMAL(10,2) NULL

-- PROC stor_report_storeintypetotal()
--   returns 7 columns:
--     carseries VARCHAR(100)
--     cartype VARCHAR(50)
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     colorcode VARCHAR(50) NULL
--     colorname VARCHAR(100) NULL
--     colorcount INT NULL

-- PROC stor_report_storeintotal()
--   returns 10 columns:
--     storeplace VARCHAR(50)
--     carseries VARCHAR(100)
--     billdate DATETIME
--     indate DATETIME
--     cartype VARCHAR(50)
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     colorcode VARCHAR(50) NULL
--     vin VARCHAR(50)
--     inprice DECIMAL(10,2)

-- PROC stor_chart_salecartype(@startdate DateTime, @enddate DateTime)
--   returns 4 columns:
--     carseries VARCHAR(100) NULL
--     onroadcount INT NULL
--     storeincount INT NULL
--     storeoutcount INT NULL

-- PROC stor_chart_salekindcount(@startdate DateTime, @enddate DateTime)
--   returns 4 columns:
--     carseries VARCHAR(100) NULL
--     sale1 INT NULL
--     sale2 INT NULL
--     sale3 INT NULL

-- PROC stor_chart_salecount(@startdate DateTime, @enddate DateTime)
--   returns 2 columns:
--     handlername VARCHAR(50)
--     totalcount INT NULL

-- PROC stor_chart_saleregion(@startdate DateTime, @enddate DateTime)
--   returns 2 columns:
--     saleregion VARCHAR(100)
--     totalcount INT NULL

-- PROC stor_chart_salecustsjob(@startdate DateTime, @enddate DateTime)
--   returns 2 columns:
--     customerjobkind VARCHAR(50)
--     totalcount INT NULL

-- PROC stor_report_storeoutreserve()
--   returns 16 columns:
--     carseries VARCHAR(100)
--     cartype VARCHAR(50)
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     colorcode VARCHAR(50) NULL
--     outdate DATETIME
--     salecompany VARCHAR(100)
--     vin VARCHAR(50)
--     saleplace VARCHAR(50)
--     votecost DECIMAL(10,2)
--     otherprice1 DECIMAL(10,2) NULL
--     otherprice2 DECIMAL(10,2) NULL
--     otherprice3 DECIMAL(10,2) NULL
--     otherprice4 DECIMAL(10,2) NULL
--     interestprice DECIMAL(10,2) NULL
--     pricediff DECIMAL(10,2) NULL

-- PROC stor_report_storeindetail(@startdate DateTime, @enddate DateTime)
--   returns 10 columns:
--     carseries VARCHAR(100)
--     indate DATETIME
--     cartype VARCHAR(50)
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     colorcode VARCHAR(50) NULL
--     vin VARCHAR(50)
--     engineno VARCHAR(50)
--     inprice DECIMAL(10,2)
--     intype VARCHAR(100)

-- PROC stor_report_storeintypetotalperiod(@startdate DateTime, @enddate DateTime)
--   returns 10 columns:
--     carseries VARCHAR(100)
--     indate DATETIME
--     cartype VARCHAR(50)
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     colorcode VARCHAR(50) NULL
--     vin VARCHAR(50)
--     engineno VARCHAR(50)
--     inprice DECIMAL(10,2)
--     intype VARCHAR(100)

-- PROC stor_report_profitdetail(@startdate DateTime, @enddate DateTime)
--   returns 11 columns:
--     carseries VARCHAR(100)
--     cartype VARCHAR(50)
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     colorcode VARCHAR(50) NULL
--     colorcount INT NULL
--     inpricesum DECIMAL(10,2) NULL
--     profitvalavg DECIMAL(10,2) NULL
--     profitvalsum DECIMAL(10,2) NULL
--     outstorepriceavg DECIMAL(10,2) NULL
--     propvalavg DECIMAL(10,2) NULL

-- PROC stor_report_storeoutcount(@startdate DateTime, @enddate DateTime)
--   returns 10 columns:
--     carseries VARCHAR(100) NULL
--     cartype VARCHAR(50) NULL
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     count1 INT NULL
--     count2 INT NULL
--     curmonthcount INT NULL
--     allcount INT NULL
--     outprice DECIMAL(10,2) NULL
--     diffprice DECIMAL(10,2) NULL

-- PROC stor_report_wholesaletotal(@startdate DateTime, @enddate DateTime)
--   returns 8 columns:
--     carseries VARCHAR(100) NULL
--     cartype VARCHAR(50) NULL
--     subsets VARCHAR(50) NULL
--     insidesetcode VARCHAR(50) NULL
--     retailcount INT NULL
--     salecount INT NULL
--     customcount INT NULL
--     allcount INT NULL

-- PROC stor_report_carseriesdetail(@companyno Int32, @startdate DateTime, @enddate DateTime)
--   returns 6 columns:
--     carseries VARCHAR(100) NULL
--     cartype VARCHAR(50) NULL
--     onroadcount INT NULL
--     storeincount1 INT NULL
--     storeincount2 INT NULL
--     storeoutcount INT NULL

-- PROC stor_report_carseriestotal(@companyno Int32, @startdate DateTime, @enddate DateTime)
--   returns 5 columns:
--     carseries VARCHAR(100) NULL
--     onroadcount INT NULL
--     storeincount1 INT NULL
--     storeincount2 INT NULL
--     storeoutcount INT NULL

