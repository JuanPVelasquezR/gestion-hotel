-- Crear la base de datos
CREATE DATABASE HotelReservas;
GO

USE HotelReservas;
GO

-- Tabla Huésped
CREATE TABLE Huesped (
    id_huesped INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL,
    apellido NVARCHAR(50) NOT NULL,
    tipo_documento NVARCHAR(20) NOT NULL,
    numero_documento VARCHAR(20) UNIQUE NOT NULL,
    telefono VARCHAR(20),
    correo NVARCHAR(100),
    direccion NVARCHAR(100)
);

-- Tabla Habitación
CREATE TABLE Habitacion (
    id_habitacion INT IDENTITY(1,1) PRIMARY KEY,
    numero VARCHAR(10) UNIQUE NOT NULL,
    tipo NVARCHAR(30) NOT NULL,
    descripcion NVARCHAR(200),
    capacidad INT NOT NULL,
    precio_noche DECIMAL(10,2) NOT NULL,
    estado NVARCHAR(20) NOT NULL
);

-- Tabla Reserva
CREATE TABLE Reserva (
    id_reserva INT IDENTITY(1,1) PRIMARY KEY,
    fecha_reserva DATETIME DEFAULT GETDATE(),
    fecha_ingreso DATE NOT NULL,
    fecha_salida DATE NOT NULL,
    estado NVARCHAR(20) NOT NULL,
    id_huesped INT NOT NULL,
    FOREIGN KEY (id_huesped) REFERENCES Huesped(id_huesped)
);

-- Tabla DetalleReserva (N:M entre Reserva y Habitación)
CREATE TABLE DetalleReserva (
    id_detalle INT IDENTITY(1,1) PRIMARY KEY,
    id_reserva INT NOT NULL,
    id_habitacion INT NOT NULL,
    precio_unitario DECIMAL(10,2) NOT NULL,
    cantidad_noches INT NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (id_reserva) REFERENCES Reserva(id_reserva),
    FOREIGN KEY (id_habitacion) REFERENCES Habitacion(id_habitacion)
);

-- Tabla Pago
CREATE TABLE Pago (
    id_pago INT IDENTITY(1,1) PRIMARY KEY,
    fecha_pago DATETIME DEFAULT GETDATE(),
    monto DECIMAL(10,2) NOT NULL,
    metodo_pago NVARCHAR(50) NOT NULL,
    id_reserva INT NOT NULL,
    FOREIGN KEY (id_reserva) REFERENCES Reserva(id_reserva)
);

-- Tabla Empleado
CREATE TABLE Empleado (
    id_empleado INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(50) NOT NULL,
    apellido NVARCHAR(50) NOT NULL,
    cargo NVARCHAR(50) NOT NULL,
    telefono VARCHAR(20),
    correo NVARCHAR(100)
);

-- Tabla ServicioExtra
CREATE TABLE ServicioExtra (
    id_servicio INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    descripcion NVARCHAR(200),
    precio DECIMAL(10,2) NOT NULL
);

-- Tabla ReservaServicio (N:M entre Reserva y ServicioExtra)
CREATE TABLE ReservaServicio (
    id_reserva_servicio INT IDENTITY(1,1) PRIMARY KEY,
    id_reserva INT NOT NULL,
    id_servicio INT NOT NULL,
    cantidad INT NOT NULL,
    subtotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (id_reserva) REFERENCES Reserva(id_reserva),
    FOREIGN KEY (id_servicio) REFERENCES ServicioExtra(id_servicio)
);


select * from Huesped