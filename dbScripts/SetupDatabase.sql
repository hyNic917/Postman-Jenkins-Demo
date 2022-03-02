IF EXISTS (
  SELECT *
	FROM sys.tables
	JOIN sys.schemas
	  ON sys.tables.schema_id = sys.schemas.schema_id
   WHERE sys.schemas.name = N'dbo'
	 AND sys.tables.name = N'test_case_item'
)
  DROP TABLE test_case_item
GO

IF EXISTS (
  SELECT *
	FROM sys.tables
	JOIN sys.schemas
	  ON sys.tables.schema_id = sys.schemas.schema_id
   WHERE sys.schemas.name = N'dbo'
	 AND sys.tables.name = N'test_case'
)
  DROP TABLE test_case
GO

IF EXISTS (
  SELECT *
	FROM sys.tables
	JOIN sys.schemas
	  ON sys.tables.schema_id = sys.schemas.schema_id
   WHERE sys.schemas.name = N'dbo'
	 AND sys.tables.name = N'batch_run'
)
  DROP TABLE batch_run
GO


CREATE TABLE batch_run (
	BatchId INT IDENTITY PRIMARY KEY,
	Description VARCHAR(500),
	Status VARCHAR(12) DEFAULT ('Active')
)

CREATE TABLE test_case(
	TestCaseId INT IDENTITY PRIMARY KEY,
	Name VARCHAR(250),
	Description VARCHAR(MAX),
	AppExternalId VARCHAR(255),
	Status VARCHAR(30),
	BatchId INT NOT NULL
)

ALTER TABLE test_case
ADD CONSTRAINT FK_test_case_BatchId FOREIGN KEY (BatchId) REFERENCES batch_run(BatchId)
GO

CREATE TABLE test_case_item (
	ItemId INT IDENTITY PRIMARY KEY,
	TestCaseId INT NOT NULL,
	Result CHAR(1)
)

ALTER TABLE test_case_item
ADD CONSTRAINT FK_test_case_item_TestCaseId FOREIGN KEY (TestCaseId) REFERENCES test_case(TestCaseId)
GO