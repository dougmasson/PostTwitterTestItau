CREATE DATABASE BaseTestItau
GO

USE BaseTestItau
GO

CREATE TABLE status
( 
  id int PRIMARY KEY,
  descricao varchar(30) NOT NULL
);

CREATE TABLE hashTag
(
	id int IDENTITY(1,1) NOT NULL,
	descricao varchar(30) NOT NULL,
	idStatus int NOT NULL,

	PRIMARY KEY (id),
	CONSTRAINT FK_HashTagStatus FOREIGN KEY (idStatus) REFERENCES status(id)
);

CREATE TABLE execucao
( 
  id int IDENTITY(1,1) NOT NULL,
  dataexecucao datetime  NOT NULL,
  usuario varchar(150) NOT NULL,
  idStatus int NOT NULL,

  PRIMARY KEY (id),
  CONSTRAINT FK_ExecucaoStatus FOREIGN KEY (idStatus) REFERENCES status(id)
);

CREATE TABLE twitters
( 
  id int IDENTITY(1,1) NOT NULL,
  texto varchar(400) NOT NULL,
  datatwitte datetime NOT NULL,
  idioma varchar(50) NOT NULL,
  usuario varchar(150) NOT NULL,
  qtdseguidores int NOT NULL,

  idExecucao int NOT NULL,
  idHashTag int NOT NULL,

  PRIMARY KEY (id),
  CONSTRAINT FK_TwitteExecucao FOREIGN KEY (idExecucao) REFERENCES execucao(id),
  CONSTRAINT FK_TwitteHashTag FOREIGN KEY (idHashTag) REFERENCES HashTag(id),
);

INSERT INTO status (id, descricao) VALUES (1, 'Ativo');
INSERT INTO status (id, descricao) VALUES (2, 'Inativo');
INSERT INTO status (id, descricao) VALUES (5, 'Concluido');
INSERT INTO status (id, descricao) VALUES (6, 'Erro');
INSERT INTO status (id, descricao) VALUES (7, 'Em Processamento');

INSERT INTO hashTag (descricao, idStatus) VALUES ('#openbanking', 1);
INSERT INTO hashTag (descricao, idStatus) VALUES ('#apifirst', 1);
INSERT INTO hashTag (descricao, idStatus) VALUES ('#devops', 1);
INSERT INTO hashTag (descricao, idStatus) VALUES ('#cloudfirst', 1);
INSERT INTO hashTag (descricao, idStatus) VALUES ('#microservices', 1);
INSERT INTO hashTag (descricao, idStatus) VALUES ('#apigateway', 1);
INSERT INTO hashTag (descricao, idStatus) VALUES ('#oauth', 1);
INSERT INTO hashTag (descricao, idStatus) VALUES ('#swagger', 1);
INSERT INTO hashTag (descricao, idStatus) VALUES ('#raml', 1);
INSERT INTO hashTag (descricao, idStatus) VALUES ('#openapis', 1);

CREATE PROCEDURE ExibirUsuarioMaioresSeguidos
AS
	SELECT ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS num, a.*
	FROM (
		   SELECT DISTINCT TOP(5)
			      e.dataexecucao,
			      t.usuario, 
			      qtdseguidores 
		   FROM Twitters t
			 INNER JOIN execucao e ON e.id = t.idExecucao
		   WHERE t.idExecucao = (SELECT TOP(1) id FROM execucao WHERE idstatus = 5 ORDER BY dataexecucao DESC)
		   ORDER BY qtdseguidores DESC 
	    ) a
GO

----------------

CREATE PROCEDURE TotalPostagemPorHora
AS
	SELECT 
			e.dataexecucao,
			CONVERT(SMALLDATETIME, CAST(datatwitte as DATE)) as 'data', 
			CAST(DATEPART(Hour, datatwitte) as varchar) + ':00' as 'hora', 
			COUNT(t.id) as 'total'
	FROM Twitters t
		INNER JOIN execucao e ON e.id = t.idExecucao
	WHERE t.idExecucao = (SELECT TOP(1) id FROM execucao WHERE idstatus = 5 ORDER BY dataexecucao DESC)
	GROUP BY e.dataexecucao, CAST(datatwitte as DATE), DATEPART(Hour, datatwitte)
	ORDER BY CAST(datatwitte as DATE) DESC,  DATEPART(Hour, datatwitte) DESC
GO

----------------

CREATE PROCEDURE TotalPostagemHashTagPorIdioma
AS
	SELECT 	
			e.dataexecucao,
			h.descricao, 
			idioma,
			COUNT(t.id) as 'total'
	FROM Twitters t
		INNER JOIN hashTag h ON h.id = t.idHashTag
		INNER JOIN execucao e ON e.id = t.idExecucao
	WHERE t.idExecucao = (SELECT TOP(1) id FROM execucao WHERE idstatus = 5 ORDER BY dataexecucao DESC)
	GROUP BY e.dataexecucao, h.descricao, idioma
	ORDER BY h.descricao
GO
