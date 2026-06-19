-- Adds the AuditLogs table used by the dashboard and entity change tracking.
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'AuditLogs')
BEGIN
    CREATE TABLE [dbo].[AuditLogs] (
        [Id]           INT            IDENTITY (1, 1) NOT NULL,
        [Timestamp]    DATETIME2      NOT NULL,
        [ActionType]   NVARCHAR (50)  NOT NULL,
        [EntityType]   NVARCHAR (100) NOT NULL,
        [EntityId]     NVARCHAR (100) NOT NULL,
        [Details]      NVARCHAR (MAX) NOT NULL,
        [PerformedBy]  NVARCHAR (256) NOT NULL,
        CONSTRAINT [PK_AuditLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
    );

    CREATE NONCLUSTERED INDEX [IX_AuditLogs_Timestamp]
        ON [dbo].[AuditLogs]([Timestamp] DESC);

    CREATE NONCLUSTERED INDEX [IX_AuditLogs_EntityType_EntityId]
        ON [dbo].[AuditLogs]([EntityType], [EntityId]);
END
