CREATE PROC spGetPlayers (@orderBy Nvarchar, @dir int)
AS
BEGIN
SELECT Pl_ID AS ID, Pl_First_name AS [First name], Pl_Surname AS Surname, Pl_Position AS Position, Te_Name  AS Team 
From Tbl_Player 
INNER JOIN Tbl_Team 
ON Te_ID = Pl_Team 
ORDER BY 
	CASE WHEN @orderBy = 'first' AND @dir = 0 THEN Convert(nvarchar(10), Pl_First_name) + Convert(nvarchar(10), Pl_Surname) END,
	CASE WHEN @orderBy = 'first' AND @dir = 1 THEN Convert(nvarchar(10), Pl_First_name) + Convert(nvarchar(10), Pl_Surname) END DESC,
	CASE WHEN @orderBy = 'last' AND @dir= 0 THEN Convert(nvarchar(10), Pl_Surname) + Convert(nvarchar(10), Pl_First_name) END,
	CASE WHEN @orderBy = 'last' AND @dir= 1 THEN Convert(nvarchar(10), Pl_Surname) + Convert(nvarchar(10), Pl_First_name) END DESC,
	CASE WHEN @orderBy = 'pos' AND @dir= 0 THEN Pl_Position END,
	CASE WHEN @orderBy = 'pos' AND @dir= 1 THEN Pl_Position END DESC,
	CASE WHEN @orderBy = 'team' AND @dir= 0 THEN Te_Name END,
	CASE WHEN @orderBy = 'team' AND @dir= 1 THEN Te_Name END DESC
END

spGetPlayers 'last',1;