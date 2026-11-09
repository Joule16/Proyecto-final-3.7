SET NAMES utf8mb4;

CREATE DATABASE IF NOT EXISTS Intercambio_libros
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_general_ci;

USE Intercambio_libros;

CREATE TABLE IF NOT EXISTS Administrador
(
    IdAdministrador INT          NOT NULL,
    Nombre          VARCHAR(60)  NOT NULL,
    Apellido        VARCHAR(60)  NOT NULL,
    UsuarioAcceso   VARCHAR(30)  NOT NULL,
    Contrasena      VARCHAR(255) NOT NULL,
    NivelAcceso     TINYINT      NOT NULL,
    FechaRegistro   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EsActivo        BOOLEAN      NOT NULL DEFAULT 1,

    CONSTRAINT PK_Administrador        PRIMARY KEY (IdAdministrador),
    CONSTRAINT UQ_Administrador_Acceso UNIQUE (UsuarioAcceso),
    CONSTRAINT CK_Administrador_Id     CHECK (IdAdministrador >= 0),
    CONSTRAINT CK_Administrador_Nombre CHECK (CHAR_LENGTH(TRIM(Nombre)) > 0),
    CONSTRAINT CK_Administrador_Apell  CHECK (CHAR_LENGTH(TRIM(Apellido)) > 0),
    CONSTRAINT CK_Administrador_Acceso CHECK (CHAR_LENGTH(TRIM(UsuarioAcceso)) >= 4),
    CONSTRAINT CK_Administrador_Contra CHECK (CHAR_LENGTH(Contrasena) >= 6),
    CONSTRAINT CK_Administrador_Nivel  CHECK (NivelAcceso BETWEEN 1 AND 3)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE IF NOT EXISTS Autor
(
    IdAutor         INT          NOT NULL,
    Nombre          VARCHAR(60)  NOT NULL,
    Apellido        VARCHAR(60)  NOT NULL,
    Nacionalidad    VARCHAR(60)  NOT NULL,
    FechaNacimiento DATE         NOT NULL,
    RutaImagen      VARCHAR(260) NOT NULL DEFAULT '',
    FechaRegistro   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EsActivo        BOOLEAN      NOT NULL DEFAULT 1,

    CONSTRAINT PK_Autor        PRIMARY KEY (IdAutor),
    CONSTRAINT CK_Autor_Id     CHECK (IdAutor >= 0),
    CONSTRAINT CK_Autor_Nombre CHECK (CHAR_LENGTH(TRIM(Nombre)) > 0),
    CONSTRAINT CK_Autor_Apell  CHECK (CHAR_LENGTH(TRIM(Apellido)) > 0),
    CONSTRAINT CK_Autor_Nacion CHECK (CHAR_LENGTH(TRIM(Nacionalidad)) > 0)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE IF NOT EXISTS Categoria
(
    IdCategoria        INT          NOT NULL,
    Nombre             VARCHAR(60)  NOT NULL,
    Descripcion        VARCHAR(200) NOT NULL DEFAULT '',
    RestringidaMenores BOOLEAN      NOT NULL DEFAULT 0,
    FechaRegistro      DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EsActivo           BOOLEAN      NOT NULL DEFAULT 1,

    CONSTRAINT PK_Categoria        PRIMARY KEY (IdCategoria),
    CONSTRAINT CK_Categoria_Id     CHECK (IdCategoria >= 0),
    CONSTRAINT CK_Categoria_Nombre CHECK (CHAR_LENGTH(TRIM(Nombre)) > 0)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE IF NOT EXISTS Editorial
(
    IdEditorial   INT          NOT NULL,
    Nombre        VARCHAR(100) NOT NULL,
    PaisOrigen    VARCHAR(60)  NOT NULL,
    AnioFundacion SMALLINT     NOT NULL,
    FechaRegistro DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EsActivo      BOOLEAN      NOT NULL DEFAULT 1,

    CONSTRAINT PK_Editorial        PRIMARY KEY (IdEditorial),
    CONSTRAINT CK_Editorial_Id     CHECK (IdEditorial >= 0),
    CONSTRAINT CK_Editorial_Nombre CHECK (CHAR_LENGTH(TRIM(Nombre)) > 0),
    CONSTRAINT CK_Editorial_Pais   CHECK (CHAR_LENGTH(TRIM(PaisOrigen)) > 0),
    CONSTRAINT CK_Editorial_Anio   CHECK (AnioFundacion >= 1400)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE IF NOT EXISTS Libro
(
    IdLibro         INT          NOT NULL,
    Titulo          VARCHAR(200) NOT NULL,
    Isbn            VARCHAR(13)  NOT NULL,
    IdAutor         INT          NOT NULL,
    IdCategoria     INT          NOT NULL,
    IdEditorial     INT          NOT NULL,
    AnioPublicacion SMALLINT     NOT NULL,
    PuntoEntrega    VARCHAR(100) NOT NULL DEFAULT '',
    FechaRegistro   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EsActivo        BOOLEAN      NOT NULL DEFAULT 1,

    CONSTRAINT PK_Libro           PRIMARY KEY (IdLibro),
    CONSTRAINT UQ_Libro_Isbn      UNIQUE (Isbn),
    CONSTRAINT FK_Libro_Autor     FOREIGN KEY (IdAutor)     REFERENCES Autor (IdAutor),
    CONSTRAINT FK_Libro_Categoria FOREIGN KEY (IdCategoria) REFERENCES Categoria (IdCategoria),
    CONSTRAINT FK_Libro_Editorial FOREIGN KEY (IdEditorial) REFERENCES Editorial (IdEditorial),
    CONSTRAINT CK_Libro_Id        CHECK (IdLibro >= 0),
    CONSTRAINT CK_Libro_Titulo    CHECK (CHAR_LENGTH(TRIM(Titulo)) > 0),
    CONSTRAINT CK_Libro_Isbn      CHECK (CHAR_LENGTH(Isbn) IN (10, 13)),
    CONSTRAINT CK_Libro_Anio      CHECK (AnioPublicacion >= 1400)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE IF NOT EXISTS Ejemplar
(
    IdEjemplar       INT         NOT NULL,
    IdLibro          INT         NOT NULL,
    CodigoInventario VARCHAR(12) NOT NULL,
    Condicion        VARCHAR(10) NOT NULL DEFAULT 'Nuevo',
    Disponible       BOOLEAN     NOT NULL DEFAULT 1,
    FechaRegistro    DATETIME    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EsActivo         BOOLEAN     NOT NULL DEFAULT 1,

    CONSTRAINT PK_Ejemplar           PRIMARY KEY (IdEjemplar),
    CONSTRAINT UQ_Ejemplar_Codigo    UNIQUE (CodigoInventario),
    CONSTRAINT FK_Ejemplar_Libro     FOREIGN KEY (IdLibro) REFERENCES Libro (IdLibro),
    CONSTRAINT CK_Ejemplar_Id        CHECK (IdEjemplar >= 0),
    CONSTRAINT CK_Ejemplar_Codigo    CHECK (CHAR_LENGTH(TRIM(CodigoInventario)) > 0),
    CONSTRAINT CK_Ejemplar_Condicion CHECK (Condicion IN ('Nuevo', 'Bueno', 'Regular', 'Dañado'))
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE IF NOT EXISTS Usuario
(
    IdUsuario         INT          NOT NULL,
    Nombre            VARCHAR(60)  NOT NULL,
    Apellido          VARCHAR(60)  NOT NULL,
    Email             VARCHAR(100) NOT NULL,
    Telefono          CHAR(10)     NOT NULL,
    IdUniversitario   VARCHAR(20)  NOT NULL,
    ContadorSanciones INT          NOT NULL DEFAULT 0,
    EstaBaneado       BOOLEAN      NOT NULL DEFAULT 0,
    MotivoBaneo       VARCHAR(200) NOT NULL DEFAULT '',
    Rol               TINYINT      NOT NULL DEFAULT 0,
    ContrasenaHash    VARCHAR(255) NOT NULL DEFAULT '',
    FechaRegistro     DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EsActivo          BOOLEAN      NOT NULL DEFAULT 1,

    CONSTRAINT PK_Usuario          PRIMARY KEY (IdUsuario),
    CONSTRAINT UQ_Usuario_Email    UNIQUE (Email),
    CONSTRAINT UQ_Usuario_IdUniv   UNIQUE (IdUniversitario),
    CONSTRAINT CK_Usuario_Id       CHECK (IdUsuario >= 0),
    CONSTRAINT CK_Usuario_Nombre   CHECK (CHAR_LENGTH(TRIM(Nombre)) > 0),
    CONSTRAINT CK_Usuario_Apell    CHECK (CHAR_LENGTH(TRIM(Apellido)) > 0),
    CONSTRAINT CK_Usuario_Email    CHECK (Email LIKE '%@%.%'),
    CONSTRAINT CK_Usuario_Telefono CHECK (Telefono REGEXP '^[0-9]{10}$'),
    CONSTRAINT CK_Usuario_IdUniv   CHECK (CHAR_LENGTH(TRIM(IdUniversitario)) > 0),
    CONSTRAINT CK_Usuario_Contador CHECK (ContadorSanciones >= 0),
    CONSTRAINT CK_Usuario_Rol      CHECK (Rol IN (0, 1))
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE IF NOT EXISTS Prestamo
(
    IdPrestamo          INT      NOT NULL,
    IdUsuario           INT      NOT NULL,
    IdEjemplar          INT      NOT NULL,
    FechaPrestamo       DATE     NOT NULL,
    FechaLimite         DATE     NOT NULL,
    FechaDevolucionReal DATE     NULL,
    Devuelto            BOOLEAN  NOT NULL DEFAULT 0,
    FechaRegistro       DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EsActivo            BOOLEAN  NOT NULL DEFAULT 1,

    CONSTRAINT PK_Prestamo          PRIMARY KEY (IdPrestamo),
    CONSTRAINT FK_Prestamo_Usuario  FOREIGN KEY (IdUsuario)  REFERENCES Usuario (IdUsuario),
    CONSTRAINT FK_Prestamo_Ejemplar FOREIGN KEY (IdEjemplar) REFERENCES Ejemplar (IdEjemplar),
    CONSTRAINT CK_Prestamo_Id       CHECK (IdPrestamo >= 0),
    CONSTRAINT CK_Prestamo_Limite   CHECK (FechaLimite >= FechaPrestamo),
    CONSTRAINT CK_Prestamo_Devol    CHECK (FechaDevolucionReal IS NULL OR FechaDevolucionReal >= FechaPrestamo)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE IF NOT EXISTS DetallePrestamo
(
    IdPrestamo INT NOT NULL,
    IdEjemplar INT NOT NULL,

    CONSTRAINT PK_DetallePrestamo          PRIMARY KEY (IdPrestamo, IdEjemplar),
    CONSTRAINT FK_DetallePrestamo_Prestamo FOREIGN KEY (IdPrestamo) REFERENCES Prestamo (IdPrestamo) ON DELETE CASCADE,
    CONSTRAINT FK_DetallePrestamo_Ejemplar FOREIGN KEY (IdEjemplar) REFERENCES Ejemplar (IdEjemplar)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE IF NOT EXISTS HistorialPrestamo
(
    IdHistorial INT          NOT NULL AUTO_INCREMENT,
    IdPrestamo  INT          NOT NULL,
    Accion      VARCHAR(255) NOT NULL,
    FechaHora   DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT PK_HistorialPrestamo          PRIMARY KEY (IdHistorial),
    CONSTRAINT FK_HistorialPrestamo_Prestamo FOREIGN KEY (IdPrestamo) REFERENCES Prestamo (IdPrestamo) ON DELETE CASCADE,
    CONSTRAINT CK_HistorialPrestamo_Accion   CHECK (CHAR_LENGTH(TRIM(Accion)) > 0)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE IF NOT EXISTS Sancion
(
    IdSancion                      INT           NOT NULL,
    IdUsuario                      INT           NOT NULL,
    IdPrestamo                     INT           NOT NULL,
    Motivo                         VARCHAR(200)  NOT NULL,
    DiasRetraso                    INT           NOT NULL,
    PorcentajePenalizacionCondicion DECIMAL(3,2) NOT NULL DEFAULT 0,
    Monto                          DECIMAL(10,2) AS (ROUND(DiasRetraso * 10 + IF(DiasRetraso > 0, DiasRetraso * 10, 10) * PorcentajePenalizacionCondicion, 2)) PERSISTENT,
    FechaInicio                    DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FechaFinDesactivacion          DATETIME      NOT NULL,
    Pagada                         BOOLEAN       NOT NULL DEFAULT 0,
    FechaRegistro                  DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EsActivo                       BOOLEAN       NOT NULL DEFAULT 1,

    CONSTRAINT PK_Sancion          PRIMARY KEY (IdSancion),
    CONSTRAINT FK_Sancion_Usuario  FOREIGN KEY (IdUsuario)  REFERENCES Usuario (IdUsuario),
    CONSTRAINT FK_Sancion_Prestamo FOREIGN KEY (IdPrestamo) REFERENCES Prestamo (IdPrestamo),
    CONSTRAINT CK_Sancion_Id       CHECK (IdSancion >= 0),
    CONSTRAINT CK_Sancion_Motivo   CHECK (CHAR_LENGTH(TRIM(Motivo)) > 0),
    CONSTRAINT CK_Sancion_Dias     CHECK (DiasRetraso >= 0),
    CONSTRAINT CK_Sancion_Porcent  CHECK (PorcentajePenalizacionCondicion BETWEEN 0 AND 1),
    CONSTRAINT CK_Sancion_Fechas   CHECK (FechaFinDesactivacion >= FechaInicio)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE IF NOT EXISTS Pago
(
    IdPago        INT           NOT NULL,
    IdSancion     INT           NOT NULL,
    Monto         DECIMAL(10,2) NOT NULL,
    FechaPago     DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    MetodoPago    VARCHAR(15)   NOT NULL DEFAULT 'Efectivo',
    FechaRegistro DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EsActivo      BOOLEAN       NOT NULL DEFAULT 1,

    CONSTRAINT PK_Pago         PRIMARY KEY (IdPago),
    CONSTRAINT FK_Pago_Sancion FOREIGN KEY (IdSancion) REFERENCES Sancion (IdSancion),
    CONSTRAINT CK_Pago_Id      CHECK (IdPago >= 0),
    CONSTRAINT CK_Pago_Monto   CHECK (Monto > 0),
    CONSTRAINT CK_Pago_Metodo  CHECK (MetodoPago IN ('Efectivo', 'Tarjeta', 'Transferencia'))
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE TABLE IF NOT EXISTS Intercambio
(
    IdIntercambio       INT          NOT NULL,
    IdUsuarioSolicitante INT         NOT NULL,
    IdLibroSolicitado   INT          NOT NULL,
    IdLibroOfrecido     INT          NULL,
    FechaSolicitud      DATE         NOT NULL,
    FechaConclusion     DATE         NULL,
    Estatus             TINYINT      NOT NULL DEFAULT 0,
    PuntoEncuentro      VARCHAR(200) NOT NULL DEFAULT '',
    FechaRegistro       DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    EsActivo            BOOLEAN      NOT NULL DEFAULT 1,

    CONSTRAINT PK_Intercambio           PRIMARY KEY (IdIntercambio),
    CONSTRAINT FK_Intercambio_Usuario   FOREIGN KEY (IdUsuarioSolicitante) REFERENCES Usuario (IdUsuario),
    CONSTRAINT FK_Intercambio_Solicitado FOREIGN KEY (IdLibroSolicitado)   REFERENCES Libro (IdLibro),
    CONSTRAINT FK_Intercambio_Ofrecido  FOREIGN KEY (IdLibroOfrecido)      REFERENCES Libro (IdLibro),
    CONSTRAINT CK_Intercambio_Id        CHECK (IdIntercambio >= 0),
    CONSTRAINT CK_Intercambio_Estatus   CHECK (Estatus BETWEEN 0 AND 4),
    CONSTRAINT CK_Intercambio_Libros    CHECK (IdLibroOfrecido IS NULL OR IdLibroOfrecido <> IdLibroSolicitado),
    CONSTRAINT CK_Intercambio_Concluido CHECK ((Estatus = 2 AND FechaConclusion IS NOT NULL) OR (Estatus <> 2 AND FechaConclusion IS NULL)),
    CONSTRAINT CK_Intercambio_Fechas    CHECK (FechaConclusion IS NULL OR FechaConclusion >= FechaSolicitud)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4;

CREATE OR REPLACE VIEW vw_PrestamosConRetraso AS
SELECT  P.IdPrestamo,
        P.IdUsuario,
        CONCAT(U.Nombre, ' ', U.Apellido) AS Usuario,
        P.IdEjemplar,
        P.FechaLimite,
        P.FechaDevolucionReal,
        DATEDIFF(IFNULL(P.FechaDevolucionReal, CURDATE()), P.FechaLimite) AS DiasRetraso
FROM    Prestamo AS P
JOIN    Usuario  AS U ON U.IdUsuario = P.IdUsuario
WHERE   DATEDIFF(IFNULL(P.FechaDevolucionReal, CURDATE()), P.FechaLimite) > 0;

CREATE OR REPLACE VIEW vw_SaldoSanciones AS
SELECT  S.IdSancion,
        S.IdUsuario,
        CONCAT(U.Nombre, ' ', U.Apellido) AS Usuario,
        S.Monto,
        IFNULL(PG.TotalPagado, 0)           AS TotalPagado,
        S.Monto - IFNULL(PG.TotalPagado, 0) AS Saldo,
        S.Pagada
FROM    Sancion AS S
JOIN    Usuario AS U ON U.IdUsuario = S.IdUsuario
LEFT JOIN (SELECT IdSancion, SUM(Monto) AS TotalPagado
           FROM   Pago
           GROUP BY IdSancion) AS PG ON PG.IdSancion = S.IdSancion;

-- ==========================================
-- INSERCIÓN DE DATOS DE EJEMPLO
-- ==========================================

SET NAMES utf8mb4;

USE Intercambio_libros;

INSERT IGNORE INTO Administrador (IdAdministrador, Nombre, Apellido, UsuarioAcceso, Contrasena, NivelAcceso) VALUES
(1, 'Ariel', 'Villa',  'avilla',  'Admin2026', 3),
(2, 'Joule', 'Suñiga', 'jsuniga', 'Admin2026', 2);

INSERT IGNORE INTO Autor (IdAutor, Nombre, Apellido, Nacionalidad, FechaNacimiento) VALUES
(1, 'Gabriel', 'García Márquez', 'Colombiana', '1927-03-06'),
(2, 'Julio',   'Cortázar',       'Argentina',  '1914-08-26'),
(3, 'Isabel',  'Allende',        'Chilena',    '1942-08-02');

INSERT IGNORE INTO Categoria (IdCategoria, Nombre, Descripcion, RestringidaMenores) VALUES
(1, 'Novela', 'Narrativa de ficción de larga extensión', 0),
(2, 'Cuento', 'Narrativa breve',                         0),
(3, 'Terror', 'Historias de miedo y suspenso',           1);

INSERT IGNORE INTO Editorial (IdEditorial, Nombre, PaisOrigen, AnioFundacion) VALUES
(1, 'Sudamericana', 'Argentina', 1939),
(2, 'Planeta',      'España',    1949);

INSERT IGNORE INTO Libro (IdLibro, Titulo, Isbn, IdAutor, IdCategoria, IdEditorial, AnioPublicacion, PuntoEntrega) VALUES
(1, 'Cien años de soledad',     '9780307474728', 1, 1, 1, 1967, 'Biblioteca central'),
(2, 'Rayuela',                  '9788437604572', 2, 1, 1, 1963, 'Cafetería del campus'),
(3, 'La casa de los espíritus', '9780553383805', 3, 1, 2, 1982, 'Biblioteca central');

INSERT IGNORE INTO Ejemplar (IdEjemplar, IdLibro, CodigoInventario, Condicion, Disponible) VALUES
(1, 1, 'EJ-0001', 'Nuevo',   1),
(2, 1, 'EJ-0002', 'Bueno',   0),
(3, 2, 'EJ-0003', 'Regular', 0),
(4, 3, 'EJ-0004', 'Dañado',  1);

INSERT IGNORE INTO Usuario (IdUsuario, Nombre, Apellido, Email, Telefono, IdUniversitario, ContadorSanciones) VALUES
(1, 'María',  'López',   'maria.lopez@alumnos.udg.mx',    '3312345678', '219876543', 1),
(2, 'Carlos', 'Ramírez', 'carlos.ramirez@alumnos.udg.mx', '3398765432', '218765432', 0);

INSERT IGNORE INTO Prestamo (IdPrestamo, IdUsuario, IdEjemplar, FechaPrestamo, FechaLimite, FechaDevolucionReal, Devuelto) VALUES
(1, 1, 1, DATE_SUB(CURDATE(), INTERVAL 30 DAY), DATE_SUB(CURDATE(), INTERVAL 23 DAY), DATE_SUB(CURDATE(), INTERVAL 20 DAY), 1),
(2, 2, 2, DATE_SUB(CURDATE(), INTERVAL 5 DAY),  DATE_ADD(CURDATE(), INTERVAL 2 DAY),  NULL,                                  0),
(3, 1, 3, DATE_SUB(CURDATE(), INTERVAL 25 DAY), DATE_SUB(CURDATE(), INTERVAL 18 DAY), NULL,                                  0);

INSERT IGNORE INTO DetallePrestamo (IdPrestamo, IdEjemplar) VALUES
(1, 1),
(2, 2),
(3, 3);

INSERT INTO HistorialPrestamo (IdPrestamo, Accion)
SELECT 1, 'Usuario:1|Ejemplar:1|Prestamo:2026-10-01|Limite:2026-10-08|Devuelto:False|Activo:True' FROM DUAL
WHERE NOT EXISTS (SELECT 1 FROM HistorialPrestamo WHERE IdPrestamo = 1)
AND EXISTS (SELECT 1 FROM Prestamo WHERE IdPrestamo = 1);

INSERT INTO HistorialPrestamo (IdPrestamo, Accion)
SELECT 3, 'Usuario:1|Ejemplar:3|Prestamo:2026-10-06|Limite:2026-10-13|Devuelto:False|Activo:True' FROM DUAL
WHERE NOT EXISTS (SELECT 1 FROM HistorialPrestamo WHERE IdPrestamo = 3)
AND EXISTS (SELECT 1 FROM Prestamo WHERE IdPrestamo = 3);

INSERT IGNORE INTO Sancion (IdSancion, IdUsuario, IdPrestamo, Motivo, DiasRetraso, PorcentajePenalizacionCondicion, FechaFinDesactivacion, Pagada) VALUES
(1, 1, 1, 'Devolución tardía', 3, 0.00, DATE_ADD(NOW(), INTERVAL 15 DAY), 1);

INSERT IGNORE INTO Pago (IdPago, IdSancion, Monto, MetodoPago) VALUES
(1, 1, 30.00, 'Efectivo');

INSERT IGNORE INTO Intercambio (IdIntercambio, IdUsuarioSolicitante, IdLibroSolicitado, IdLibroOfrecido, FechaSolicitud, Estatus, PuntoEncuentro) VALUES
(1, 2, 3, 2, CURDATE(), 0, 'Biblioteca central, mostrador');
