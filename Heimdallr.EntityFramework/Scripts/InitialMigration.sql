USE HEIMDALLR
GO

CREATE TABLE [icHEIMDALLR_Audiences] (
    [Id] nvarchar(255) NOT NULL,
    [AllowedOrigin] nvarchar(255) NOT NULL,
    [ClientId] nvarchar(255) NOT NULL,
    [ClientSecret] nvarchar(255) NOT NULL,
    [HasAdministrativeAccess] bit NOT NULL,
    [IsEnabled] bit NOT NULL,
    [IsNative] bit NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [RefreshTokenLifeTime] int NOT NULL,
    CONSTRAINT [PK_icHEIMDALLR_Audiences] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [icHEIMDALLR_RefreshTokens] (
    [Id] nvarchar(255) NOT NULL,
    [ClientId] nvarchar(255) NOT NULL,
    [ExpiresUtc] datetime2 NOT NULL,
    [IssuedUtc] datetime2 NOT NULL,
    [ProtectedTicket] nvarchar(2000) NOT NULL,
    [TokenHash] nvarchar(255) NOT NULL,
    [UserName] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_icHEIMDALLR_RefreshTokens] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [icHEIMDALLR_Users] (
    [Id] nvarchar(255) NOT NULL,
    [Email] nvarchar(255) NOT NULL,
    [IsEnabled] bit NOT NULL,
    [IsLocked] bit NOT NULL,
    [LastAccessDate] datetime2 NULL,
    [PasswordHash] nvarchar(1024) NULL,
    [PersonName] nvarchar(255) NULL,
    [PersonSurname] nvarchar(255) NULL,
    [PhotoBinary] varbinary(max) NULL,
    [UserName] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_icHEIMDALLR_Users] PRIMARY KEY ([Id])
);

GO