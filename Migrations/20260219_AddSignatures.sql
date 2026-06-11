-- Add signature and timestamp columns to EmployeeTrainingTask
ALTER TABLE EmployeeTrainingTasks
ADD 
    EmployeeSignature VARBINARY(MAX) NULL, -- or NVARCHAR(MAX) for base64
    EmployeeSignatureDate DATETIME NULL,
    ManagerSignature VARBINARY(MAX) NULL, -- or NVARCHAR(MAX) for base64
    ManagerSignatureDate DATETIME NULL,
    SignatureType NVARCHAR(50) NULL; -- 'NonAdvancedComplete' or 'AdvancedComplete'

-- If you want a separate table for signatures (for audit/history):
-- CREATE TABLE EmployeeTrainingTaskSignatures (
--     Id INT IDENTITY PRIMARY KEY,
--     EmployeeTrainingTaskId INT NOT NULL,
--     EmployeeSignature VARBINARY(MAX) NULL,
--     EmployeeSignatureDate DATETIME NULL,
--     ManagerSignature VARBINARY(MAX) NULL,
--     ManagerSignatureDate DATETIME NULL,
--     SignatureType NVARCHAR(50) NOT NULL, -- 'NonAdvancedComplete' or 'AdvancedComplete'
--     CONSTRAINT FK_EmployeeTrainingTaskSignatures_EmployeeTrainingTask FOREIGN KEY (EmployeeTrainingTaskId) REFERENCES EmployeeTrainingTasks(Id)
-- );
