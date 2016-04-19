namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class importance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Note", "Importance", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Note", "Importance");
        }
    }
}
