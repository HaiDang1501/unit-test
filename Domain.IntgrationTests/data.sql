CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Follower" (
    "FollowerId" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "CreatedUtc" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Follower" PRIMARY KEY ("FollowerId")
);

CREATE TABLE "User" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Email" text NOT NULL,
    "HasPublicProfile" boolean NOT NULL,
    CONSTRAINT "PK_User" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250228033735_test', '8.0.13');

COMMIT;

