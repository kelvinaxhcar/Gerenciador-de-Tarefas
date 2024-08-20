using FluentMigrator;

namespace TaskManager.TaskApi.Domain.Migrations {
[Migration(2024081502)]
public class _2024081502_create_task_table : Migration {
  public override void Up() {
    Create.Table("Task")
        .WithColumn("Id")
        .AsInt32()
        .PrimaryKey()
        .Identity()
        .WithColumn("Title")
        .AsString()
        .NotNullable()
        .WithColumn("Description")
        .AsString()
        .NotNullable()
        .WithColumn("Completed")
        .AsBoolean()
        .NotNullable()
        .WithColumn("TeamId")
        .AsInt32()
        .NotNullable()
        .ForeignKey("Team", "Id");
  }

  public override void Down() { Delete.Table("Task"); }
}
}
