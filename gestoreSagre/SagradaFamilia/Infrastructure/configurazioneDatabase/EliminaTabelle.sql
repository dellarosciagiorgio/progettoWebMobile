DECLARE @sql NVARCHAR(MAX) = '';

-- Generare comandi per eliminare tutti i vincoli di foreign key
SELECT @sql += 'ALTER TABLE ' + QUOTENAME(OBJECT_NAME(parent_object_id)) 
               + ' DROP CONSTRAINT ' + QUOTENAME(name) + ';'
FROM sys.foreign_keys;

-- Eliminare i vincoli
EXEC sp_executesql @sql;

-- Generare ed eseguire il comando per eliminare tutte le tabelle
SET @sql = '';

SELECT @sql += 'DROP TABLE IF EXISTS ' + QUOTENAME(name) + ';'
FROM sys.tables;

EXEC sp_executesql @sql;
