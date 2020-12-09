namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Note", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Note", "Content", c => c.String(nullable: false, maxLength: 2000));
        }
    }
}
