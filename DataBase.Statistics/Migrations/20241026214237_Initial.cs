using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataBase.Statistics.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: true),
                    security_stamp = table.Column<string>(type: "text", nullable: true),
                    concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    access_failed_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    game_id = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    map = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    server = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_games", x => x.game_id);
                });

            migrationBuilder.CreateTable(
                name: "squads",
                columns: table => new
                {
                    tag = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    cadet_tag = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_squads", x => x.tag);
                });

            migrationBuilder.CreateTable(
                name: "versions",
                columns: table => new
                {
                    version_number = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_versions", x => x.version_number);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    claim_type = table.Column<string>(type: "text", nullable: true),
                    claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "text", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    steam_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tag = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_players", x => x.steam_id);
                    table.ForeignKey(
                        name: "fk_players_squads_tag",
                        column: x => x.tag,
                        principalTable: "squads",
                        principalColumn: "tag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attendance",
                columns: table => new
                {
                    unique_identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    game_id = table.Column<string>(type: "character varying(300)", nullable: false),
                    game_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_steam_id = table.Column<long>(type: "bigint", nullable: false),
                    is_dead = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attendance", x => x.unique_identifier);
                    table.ForeignKey(
                        name: "fk_attendance_players_user_steam_id",
                        column: x => x.user_steam_id,
                        principalTable: "players",
                        principalColumn: "steam_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_attendance_replays_game_id",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "game_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "game_player",
                columns: table => new
                {
                    games_game_id = table.Column<string>(type: "character varying(300)", nullable: false),
                    players_steam_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_game_player", x => new { x.games_game_id, x.players_steam_id });
                    table.ForeignKey(
                        name: "fk_game_player_players_players_steam_id",
                        column: x => x.players_steam_id,
                        principalTable: "players",
                        principalColumn: "steam_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_game_player_replays_games_game_id",
                        column: x => x.games_game_id,
                        principalTable: "games",
                        principalColumn: "game_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kills",
                columns: table => new
                {
                    unique_identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    team_kill = table.Column<bool>(type: "boolean", nullable: false),
                    weapons = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    game_id = table.Column<string>(type: "character varying(300)", nullable: false),
                    user_steam_id = table.Column<long>(type: "bigint", nullable: false),
                    user_tag = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    killed_steam_id = table.Column<long>(type: "bigint", nullable: true),
                    killed_user_steam_id = table.Column<long>(type: "bigint", nullable: true),
                    time = table.Column<int>(type: "integer", nullable: false),
                    distance = table.Column<double>(type: "double precision", nullable: false),
                    vehicle_name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    is_vehicle_killed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_kills", x => x.unique_identifier);
                    table.ForeignKey(
                        name: "fk_kills_players_killed_user_steam_id",
                        column: x => x.killed_user_steam_id,
                        principalTable: "players",
                        principalColumn: "steam_id");
                    table.ForeignKey(
                        name: "fk_kills_players_user_steam_id",
                        column: x => x.user_steam_id,
                        principalTable: "players",
                        principalColumn: "steam_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_kills_replays_game_id",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "game_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medical_info",
                columns: table => new
                {
                    unique_identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    medical_affiliation = table.Column<int>(type: "integer", nullable: false),
                    game_id = table.Column<string>(type: "character varying(300)", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    healer_steam_id = table.Column<long>(type: "bigint", nullable: false),
                    wounded_steam_id = table.Column<long>(type: "bigint", nullable: true),
                    time_second = table.Column<int>(type: "integer", nullable: false),
                    health_points_healed = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_medical_info", x => x.unique_identifier);
                    table.ForeignKey(
                        name: "fk_medical_info_games_game_id",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "game_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_medical_info_players_healer_steam_id",
                        column: x => x.healer_steam_id,
                        principalTable: "players",
                        principalColumn: "steam_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_medical_info_players_wounded_steam_id",
                        column: x => x.wounded_steam_id,
                        principalTable: "players",
                        principalColumn: "steam_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "damage",
                columns: table => new
                {
                    unique_identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    damage_value = table.Column<double>(type: "double precision", nullable: false),
                    bullet_type = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    team_kill = table.Column<bool>(type: "boolean", nullable: false),
                    weapons = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    game_id = table.Column<string>(type: "character varying(300)", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    murderer_steam_id = table.Column<long>(type: "bigint", nullable: true),
                    killed_steam_id = table.Column<long>(type: "bigint", nullable: false),
                    time = table.Column<int>(type: "integer", nullable: false),
                    distance = table.Column<double>(type: "double precision", nullable: false),
                    vehicle_name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    is_vehicle_killed = table.Column<bool>(type: "boolean", nullable: false),
                    kill_unique_identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    attendance_unique_identifier = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_damage", x => x.unique_identifier);
                    table.ForeignKey(
                        name: "fk_damage_attendance_attendance_unique_identifier",
                        column: x => x.attendance_unique_identifier,
                        principalTable: "attendance",
                        principalColumn: "unique_identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_damage_kills_kill_unique_identifier",
                        column: x => x.kill_unique_identifier,
                        principalTable: "kills",
                        principalColumn: "unique_identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_damage_replays_game_id",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "game_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "AspNetRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "AspNetUserRoles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_attendance_game_id",
                table: "attendance",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "ix_attendance_user_steam_id",
                table: "attendance",
                column: "user_steam_id");

            migrationBuilder.CreateIndex(
                name: "ix_damage_attendance_unique_identifier",
                table: "damage",
                column: "attendance_unique_identifier");

            migrationBuilder.CreateIndex(
                name: "ix_damage_game_id",
                table: "damage",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "ix_damage_kill_unique_identifier",
                table: "damage",
                column: "kill_unique_identifier");

            migrationBuilder.CreateIndex(
                name: "ix_game_player_players_steam_id",
                table: "game_player",
                column: "players_steam_id");

            migrationBuilder.CreateIndex(
                name: "ix_games_date",
                table: "games",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "ix_kills_game_id",
                table: "kills",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "ix_kills_killed_steam_id",
                table: "kills",
                column: "killed_steam_id");

            migrationBuilder.CreateIndex(
                name: "ix_kills_killed_user_steam_id",
                table: "kills",
                column: "killed_user_steam_id");

            migrationBuilder.CreateIndex(
                name: "ix_kills_user_steam_id",
                table: "kills",
                column: "user_steam_id");

            migrationBuilder.CreateIndex(
                name: "ix_medical_info_game_id",
                table: "medical_info",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "ix_medical_info_healer_steam_id",
                table: "medical_info",
                column: "healer_steam_id");

            migrationBuilder.CreateIndex(
                name: "ix_medical_info_wounded_steam_id",
                table: "medical_info",
                column: "wounded_steam_id");

            migrationBuilder.CreateIndex(
                name: "ix_players_name",
                table: "players",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_players_tag",
                table: "players",
                column: "tag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "damage");

            migrationBuilder.DropTable(
                name: "game_player");

            migrationBuilder.DropTable(
                name: "medical_info");

            migrationBuilder.DropTable(
                name: "versions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "attendance");

            migrationBuilder.DropTable(
                name: "kills");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "squads");
        }
    }
}
