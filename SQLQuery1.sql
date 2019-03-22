SELECT Pl_Surname AS Surname, Sp_Name AS Sponsor, Pl_ID AS ID
FROM ((Tbl_Player
INNER JOIN Tbl_Sponsors ON Pl_ID = Sps_Player)
INNER JOIN Tbl_Sponsor ON Sp_ID = Sps_Sponsor)
WHERE Sp_ID = 1