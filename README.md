# Setup

#### 1. Add environment variable `TASK_TRACKER_CONNECTIONSTRINGS__TASKSDB` to define connection string to the tasks db.

Sample of **permanent** environment variable:

```powershell
[System.Environment]::SetEnvironmentVariable("TASK_TRACKER_CONNECTIONSTRINGS__TASKSDB", "Host=localhost;Port=5432;Database=postgres;User Id=postgres;Password=sa;Include Error Detail=true;", "User")
```

> [!NOTE]
> Permanent environment variable does not become immediately available in the terminal or other running apps. Reading a value of newly added environment variable requires re-opening the terminal / IDE.

Sample of **session** environment variable:

```powershell
$env:TASK_TRACKER_CONNECTIONSTRINGS__TASKSDB = "Host=localhost;Port=5432;Database=postgres;User Id=postgres;Password=sa;Include Error Detail=true;"
```

#### 2. Restore db schema and seed `task_item` table with test data:

```powershell
cd <Repo Root>\src\Backend\Infrastructure\Db\
dotnet restore
dotnet ef database update
```

> [!NOTE]
> Ignore following db migrations error. This is known behavior of the Npgsql EF Core provider. Rather than check existence of `__EFMigrationsHistory` table, it runs `SELECT` query against it and handles exception to create the table when it have not been found.
><!-- Hack to display text below in red color -->
> ```diff
> - Failed executing DbCommand [Parameters=[], CommandType='Text', CommandTimeout='30']
> 
> - SELECT migration_id, product_version
> - FROM tsk."__EFMigrationsHistory"
> - ORDER BY migration_id;
> ```

#### 3. Generate development certificate for HTTPS if it does not yet exist:

```powershell
dotnet dev-certs https --trust
```

#### 4. Build and run tasks REST API:

```powershell
cd <Repo Root>\src\Backend\Api\
dotnet run --launch-profile Development
```
