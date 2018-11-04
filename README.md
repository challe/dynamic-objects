# dynamic-objects
The purpose of this project is for you to be able to simply describe your applicaton in a YAML-file, and that's it.
You then start the application, and everything is automatically set up for you:
* CLR objects are created
* Entity framework entities are created
* Tables (and their relations) are created
* A GrapQL schema is created

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

## Generated tables
* The fields `Id`, `Created`, `CreatedBy`, `Modified` and `ModifiedBy` are added automatically for all objects
* Foreign keys are created when referencing other dynamic objects (as in the example)

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

## GraphQL examples

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

### Get organization
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
