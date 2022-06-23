CREATE PROCEDURE XP_GetListTags
    @newsAlias AS NVARCHAR(200)
AS
BEGIN
    SELECT t.* 
	FROM dbo.Tags t
	JOIN dbo.ProductTags pt ON pt.TagID = t.ID
	JOIN dbo.Products p ON p.ID = pt.ProductID
	WHERE p.Name=@newsAlias
END