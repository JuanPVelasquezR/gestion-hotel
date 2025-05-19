USE HotelReservas;
GO

-- Verificar si existe la tabla Empleado y renombrarla a Usuario
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Empleado')
BEGIN
    EXEC sp_rename 'Empleado', 'Usuario';
    PRINT 'Tabla Empleado renombrada a Usuario.';
END

-- Verificar si existe la tabla Usuario
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuario')
BEGIN
    -- Crear la tabla Usuario si no existe
    CREATE TABLE Usuario (
        id_usuario INT IDENTITY(1,1) PRIMARY KEY,
        nombre NVARCHAR(50) NOT NULL,
        apellido NVARCHAR(50) NOT NULL,
        cargo NVARCHAR(50) NOT NULL,
        telefono VARCHAR(20),
        correo NVARCHAR(100),
        contrasena NVARCHAR(50) NOT NULL
    );
    PRINT 'Tabla Usuario creada.';
END
ELSE
BEGIN
    -- Verificar si la columna contraseña existe
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Usuario') AND name = 'contrasena')
    BEGIN
        -- Añadir la columna contraseña si no existe
        ALTER TABLE Usuario ADD contrasena NVARCHAR(50) NOT NULL DEFAULT '123456';
        PRINT 'Columna contrasena añadida a la tabla Usuario.';
    END
END

-- Insertar algunos datos de ejemplo si la tabla está vacía
IF NOT EXISTS (SELECT TOP 1 * FROM Usuario)
BEGIN
    INSERT INTO Usuario (nombre, apellido, cargo, telefono, correo, contrasena)
    VALUES ('Admin', 'Sistema', 'Administrador', '123456789', 'admin@hotel.com', 'admin123');
    
    INSERT INTO Usuario (nombre, apellido, cargo, telefono, correo, contrasena)
    VALUES ('Juan', 'Perez', 'Empleado', '987654321', 'juan@hotel.com', 'juan123');
    
    PRINT 'Datos de ejemplo insertados en la tabla Usuario.';
END

PRINT 'Script completado con éxito.';
