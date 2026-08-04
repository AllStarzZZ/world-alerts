using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorldAlerts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedAlertRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "AlertRules",
            columns: ["Id", "Name", "Category", "MinimumSeverity", "Keyword", "Location", "IsActive"],
            values: new object[,]
            {
                { 1L, "High Severity Global Alerts", null, 3, null, null, true },
                { 2L, "Critical Security Events", null, 4, "security", null, true },
                { 3L, "Natural Disasters in Europe", 1, 2, null, "Europe", true },
                { 4L, "Tech Industry News", 0, 1, "technology", null, true },
                { 5L, "Critical Market Movements", 3, 4, "market", null, true }
            });

            migrationBuilder.InsertData(
                table: "AlertChannels",
                columns: ["Id", "AlertRuleId", "NotificationChannelType", "DestinationValue"],
                values: new object[,]
                {
                { 1L, 1L, 0, "admin@world-alerts.com" },           // Email for rule 1
                { 2L, 1L, 1, "https://hooks.slack.com/services/T00000000/B00000000/XXXXXXXXXXXXXXXXXXXXXXXX" }, // Slack for rule 1
                { 3L, 2L, 0, "security@world-alerts.com" },        // Email for rule 2
                { 4L, 3L, 0, "disasters@world-alerts.com" },       // Email for rule 3
                { 5L, 3L, 1, "https://hooks.slack.com/services/T00000000/B00000001/YYYYYYYYYYYYYYYYYYYYYYYY" },  // Slack for rule 3
                { 6L, 4L, 1, "https://hooks.slack.com/services/T00000000/B00000002/ZZZZZZZZZZZZZZZZZZZZZZZZ" },   // Slack for rule 4
                { 7L, 5L, 0, "traders@world-alerts.com" },         // Email for rule 5
                { 8L, 5L, 1, "https://hooks.slack.com/services/T00000000/B00000003/WWWWWWWWWWWWWWWWWWWWWWWW" }   // Slack for rule 5
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "AlertRules",
            columns: ["Id", "Name", "Category", "MinimumSeverity", "Keyword", "Location", "IsActive"],
            values: new object[,]
            {
                { 1L, "High Severity Global Alerts", null, 3, null, null, true },
                { 2L, "Critical Security Events", null, 4, "security", null, true },
                { 3L, "Natural Disasters in Europe", 1, 2, null, "Europe", true },
                { 4L, "Tech Industry News", 0, 1, "technology", null, true },
                { 5L, "Critical Market Movements", 3, 4, "market", null, true }
            });

            migrationBuilder.InsertData(
                table: "AlertChannels",
                columns: ["Id", "AlertRuleId", "NotificationChannelType", "DestinationValue"],
                values: new object[,]
                {
                { 1L, 1L, 0, "admin@world-alerts.com" },           // Email for rule 1
                { 2L, 1L, 1, "https://hooks.slack.com/services/T00000000/B00000000/XXXXXXXXXXXXXXXXXXXXXXXX" }, // Slack for rule 1
                { 3L, 2L, 0, "security@world-alerts.com" },        // Email for rule 2
                { 4L, 3L, 0, "disasters@world-alerts.com" },       // Email for rule 3
                { 5L, 3L, 1, "https://hooks.slack.com/services/T00000000/B00000001/YYYYYYYYYYYYYYYYYYYYYYYY" },  // Slack for rule 3
                { 6L, 4L, 1, "https://hooks.slack.com/services/T00000000/B00000002/ZZZZZZZZZZZZZZZZZZZZZZZZ" },   // Slack for rule 4
                { 7L, 5L, 0, "traders@world-alerts.com" },         // Email for rule 5
                { 8L, 5L, 1, "https://hooks.slack.com/services/T00000000/B00000003/WWWWWWWWWWWWWWWWWWWWWWWW" }   // Slack for rule 5
                });
        }
    }
}
