using FluentMigrator;

namespace TaskManager.TeamApi.Domain.Migrations;

[Migration(2024081501)]
public class _2024081501_create_team_table : Migration {
  public override void Up() {
    Create.Table("Team")
        .WithColumn("Id")
        .AsInt32()
        .PrimaryKey()
        .Identity()
        .WithColumn("Name")
        .AsString()
        .NotNullable()
        .WithColumn("Description")
        .AsString()
        .NotNullable();
  }

  public override void Down() { Delete.Table("Team"); }
}
