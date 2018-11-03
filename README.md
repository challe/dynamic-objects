# dynamic-objects
Describe your applicaton in a YAML-file and start the WebAPI. Tables, relations and a GrapQL schema is automatically created for you.

## Example input
```yaml
name: 'CRM Light'
storage:
  server: (LocalDB)\MSSQLLocalDB
  database: DynamicObjects
objects:
  - name: Person
    description:  'A basic person object'
    fields:
      - name: Firstname
        type: Text
      - name: Lastname
        type: Text
  - name: Organization
    description:  'A basic company object'
    fields:
      - name: Name
        type: Text
      - name: Person
        type: Person
```

### Tables are automatically created
* The fields `Id`, `Created`, `CreatedBy`, `Modified` and `ModifiedBy` are added automatically for all objects
* Foreign keys are created when referencing other dynamic objects
```sql
CREATE TABLE [dbo].[Person] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Firstname]  NVARCHAR (MAX) NULL,
    [Lastname]   NVARCHAR (MAX) NULL,
    [Created]    DATETIME2 (7)  NULL,
    [CreatedBy]  INT            NULL,
    [Modified]   DATETIME2 (7)  NULL,
    [ModifiedBy] INT            NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Organization] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (MAX) NULL,
    [Created]    DATETIME2 (7)  NULL,
    [CreatedBy]  INT            NULL,
    [Modified]   DATETIME2 (7)  NULL,
    [ModifiedBy] INT            NULL,
    [PersonsId]  INT            NOT NULL,
    CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED ([Id] ASC)
);

```

### Create organization
```json
{
  "query": "mutation($entity:Input_Organization!) { organization(entity:$entity) { id name } }",
  "variables": {
    "entity": {
      "id": 0,
        "name": "Sörens El",
        "person": {
          "id": 0,
          "firstname": "Sören",
          "lastname": "Andersson"
        }
    }
  }
}
```

### View organization
```json
{
	"query": "{organization(id: 1) { id name person { id firstname lastname } created modified }}"
}
```

```json
{
    "data": {
        "organization": {
            "id": 1,
            "name": "Sörens El",
            "person": {
                "id": 1,
                "firstname": "Sören",
                "lastname": "Andersson"
            },
            "created": "2018-11-03T21:28:40.4653158+01:00",
            "modified": "2018-11-03T21:28:40.4708052+01:00"
        }
    }
}
```
