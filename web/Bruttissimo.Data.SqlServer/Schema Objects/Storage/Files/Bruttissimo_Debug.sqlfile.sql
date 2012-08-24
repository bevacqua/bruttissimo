ALTER DATABASE [$(DatabaseName)]
    ADD FILE (NAME = [Bruttissimo_Debug], FILENAME = '$(DefaultDataPath)$(DatabaseName).mdf', FILEGROWTH = 1024 KB) TO FILEGROUP [PRIMARY];

