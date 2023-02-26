-- Adminer 4.8.2-dev PostgreSQL 15.1 (Debian 15.1-1.pgdg110+1) dump


INSERT INTO "DeviceParameters" ("Id", "Name", "Unit", "Type", "CastK", "CastB", "DeviceId", "CreatedAt", "UpdatedAt") VALUES
(1,	'outside temperature',	'°C',	9,	1,	0,	1,	'2023-02-25 20:47:24.044905+00',	NULL),
(6,	'tank lavel',	'%',	6,	0.004702121,	-30.563784,	1,	'2023-02-25 21:00:53.01324+00',	NULL),
(7,	'tank temperature',	'°C',	9,	1,	0,	1,	'2023-02-25 21:02:53.829312+00',	NULL),
(8,	'passive cooling',	NULL,	0,	1,	0,	1,	'2023-02-25 21:04:35.523502+00',	NULL),
(2,	'filling valve',	NULL,	0,	1,	0,	1,	'2023-02-25 20:49:33.04782+00',	NULL),
(3,	'drain valve',	NULL,	0,	1,	1,	1,	'2023-02-25 20:49:33.04782+00',	NULL),
(4,	'tank run',	NULL,	0,	1,	2,	1,	'2023-02-25 20:49:33.04782+00',	NULL),
(5,	'tank heater',	NULL,	0,	1,	3,	1,	'2023-02-25 20:49:33.04782+00',	NULL),
(9,	'active cooling',	NULL,	0,	1,	1,	1,	'2023-02-25 21:07:06.1373+00',	NULL),
(10,	'inside temperature',	'°C',	9,	1,	0,	1,	'2023-02-25 21:09:24.762786+00',	NULL),
(11,	'air temperature',	'°C',	9,	1,	0,	1,	'2023-02-25 21:10:34.132825+00',	NULL),
(12,	'supply air temperature',	'°C',	9,	1,	0,	1,	'2023-02-25 21:12:43.804876+00',	NULL);

INSERT INTO "Devices" ("Id", "Name", "IpAddres", "Protocol", "CreatedAt", "UpdatedAt") VALUES
(1,	'Demo Tank',	'127.0.0.1',	0,	'2023-02-25 20:38:32.755471+00',	NULL);


INSERT INTO "ModbusChannels" ("Id", "DeviceAddres", "DataAddres", "FunctionCode", "Port", "Length", "ByteOrder") VALUES
(1,	1,	0,	3,	5020,	11,	1);


INSERT INTO "NetworkChannels" ("Id", "DeviceId", "CreatedAt", "UpdatedAt") VALUES
(1,	1,	'2023-02-25 20:38:55.677829+00',	NULL);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES
('20230225203632_Init',	'7.0.3');

-- 2023-02-26 06:21:39.481604+00
