-- Script para insertar datos de prueba en la tabla Habitacion
USE HotelReservas;

-- Verificar si la tabla existe, si no, crearla
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Habitacion')
BEGIN
    CREATE TABLE Habitacion (
        id_habitacion INT IDENTITY(1,1) PRIMARY KEY,
        numero VARCHAR(10) NOT NULL,
        tipo NVARCHAR(50) NOT NULL,
        descripcion NVARCHAR(500) NULL,
        capacidad INT NOT NULL,
        precio_noche DECIMAL(10, 2) NOT NULL,
        estado NVARCHAR(50) NOT NULL
    );
END

-- Limpiar datos existentes (opcional)
-- DELETE FROM Habitacion;

-- Insertar datos de prueba
INSERT INTO Habitacion (numero, tipo, descripcion, capacidad, precio_noche, estado)
VALUES 
('101', 'Individual', 'Habitación individual con vista a la ciudad', 1, 80.00, 'Disponible'),
('102', 'Individual', 'Habitación individual con baño privado', 1, 85.00, 'Disponible'),
('103', 'Individual', 'Habitación individual con escritorio de trabajo', 1, 90.00, 'Ocupada'),
('201', 'Doble', 'Habitación doble con dos camas individuales', 2, 120.00, 'Disponible'),
('202', 'Doble', 'Habitación doble con cama matrimonial', 2, 130.00, 'Reservada'),
('203', 'Doble', 'Habitación doble con vista al jardín', 2, 140.00, 'Ocupada'),
('301', 'Suite', 'Suite de lujo con sala de estar y jacuzzi', 2, 250.00, 'Disponible'),
('302', 'Suite', 'Suite ejecutiva con área de trabajo', 2, 280.00, 'En Mantenimiento'),
('401', 'Familiar', 'Habitación familiar con dos habitaciones y sala', 4, 200.00, 'Disponible'),
('402', 'Familiar', 'Habitación familiar con cocina integrada', 4, 220.00, 'Reservada'),
('501', 'Ejecutiva', 'Habitación ejecutiva con escritorio y minibar', 1, 180.00, 'Disponible'),
('502', 'Ejecutiva', 'Habitación ejecutiva con sala de reuniones', 2, 200.00, 'Fuera de Servicio');

-- Mostrar los datos insertados
SELECT * FROM Habitacion;
