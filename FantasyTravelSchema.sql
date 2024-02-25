/*  
    CREATING THE TABLES
    
    Comment in/out these commands as necessary.
*/
/*
CREATE SCHEMA [FantasyTravel];
GO

CREATE TABLE [FantasyTravel].[Places]
(
    id int PRIMARY KEY,
    name varchar(100) NOT NULL,
    description varchar(1000),
    language int NOT NULL,
    biomType int NOT NULL
); 
GO
*/

/*
    DATA CREATION (SEEDING)
*/
    /* Stephen */
INSERT INTO [FantasyTravel].[Places] (Id, Name, Description, Language, BiomType) VALUES (1,Naboo, "A lively planet with lakes, rolling plains, and green hills where vistors can see wildlife or visit the capital where the royal family lives." ,2,1);
INSERT INTO [FantasyTravel].[Places] (Id, Name, Description, Language, BiomType) VALUES (2,Hoth, "a planet full of snow and ice where vistors can ski and snowboard, but be careful to stick to the resort for saftey!", 1,2);
INSERT INTO [FantasyTravel].[Places] (Id, Name, Description, Language, BiomType) VALUES (3,Endor, "a planet of endless forests, savannahs,grasslands, and mountains.  Perfect for any nature lovers.", 2, 3);
INSERT INTO [FantasyTravel].[Places] (Id, Name, Description, Language, BiomType) VALUES (4,Bespin, "a planet of clouds with a small area of breathability called Cloud City where one can hit the casino or hop in a cloud car for a sightseeing tour of the city.", 2, 4);
INSERT INTO [FantasyTravel].[Places] (Id, Name, Description, Language, BiomType) VALUES (5,Tatooine, "A planet with vast deserts and a warm climate.  Visitors can relieve their thirst after their time in the desert at Mos Eisley Cantina but keep your belognings close and no droids!", 3, 5);
GO
    /* Ian */
INSERT INTO [FantasyTravel].[Places] (Id, Name, Description, Language, BiomType) VALUES (6, 'El Nath', 'A snowy, mountainous region in the continent of Ossyria. Home to the original guild masters of Maple World.', 5, 2);
INSERT INTO [FantasyTravel].[Places] (Id, Name, Description, Language, BiomType) VALUES (7, 'Ellinia', 'Forests lush with greenery and trees large enough to house civilizations. Fairies reside here and uphold the magic and life that protects the Maple World.', 11, 1);
INSERT INTO [FantasyTravel].[Places] (Id, Name, Description, Language, BiomType) VALUES (8, 'Ludus Lake', 'The greater region which contains the Korean Folk Town, the Omega Sector, and the Kingdom of Toys: Ludibrium. Practice caution near the clocktower in Ludibrium.', 11, 4);
INSERT INTO [FantasyTravel].[Places] (Id, Name, Description, Language, BiomType) VALUES (9, 'The Hotel Arcus', 'An arid desert region in the middle of continental Grandis. Rumor has it that there is an ancient god dormant under the sands of Arcus...', 7, 5);
INSERT INTO [FantasyTravel].[Places] (Id, Name, Description, Language, BiomType) VALUES (10, 'Sellas', 'Located beneath the mirrored memory sea of Esfera, this region is home to many Erda resembling aquatic lifeforms. The vastness of the region in combination with the luminosity of its inhabitants has granted it the moniker "Where the Stars Rest"', 0, 6);
GO

/*
    QUERYING
*/
SELECT * FROM [FantasyTravel].[Places];
GO