CREATE DATABASE webmotos;

CREATE TABLE Usuarios (
  id_usuario INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL UNIQUE,
  password VARCHAR(255) NOT NULL,
  rol ENUM('admin', 'usuario') NOT NULL,
  creado_en TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Marcas (
  id_marca INT AUTO_INCREMENT PRIMARY KEY,
  nombre_marca VARCHAR(255) NOT NULL,
  pais_origen VARCHAR(255),
  creado_en TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Tipos (
  id_tipo INT AUTO_INCREMENT PRIMARY KEY,
  tipo VARCHAR(50) UNIQUE NOT NULL
);



CREATE TABLE Modelos (
  id_modelo INT AUTO_INCREMENT PRIMARY KEY,
  id_marca INT,
  nombre_modelo VARCHAR(255) NOT NULL,
  anio INT,
  id_tipo INT NOT NULL,
  creado_en TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (id_marca) REFERENCES Marcas(id_marca),
  FOREIGN KEY (id_tipo) REFERENCES Tipos(id_tipo)
);


CREATE TABLE Motos (
  id_moto INT AUTO_INCREMENT PRIMARY KEY,
  id_modelo INT,
  precio DECIMAL(10, 2),
  color VARCHAR(50),
  cilindrada INT,
  potencia INT,
  descripcion TEXT,
  disponible BOOLEAN,
  creado_en TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (id_modelo) REFERENCES Modelos(id_modelo)
);

CREATE TABLE Fotos (
  id_foto INT AUTO_INCREMENT PRIMARY KEY,
  id_moto INT,
  url_foto VARCHAR(255) NOT NULL,
  creado_en TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (id_moto) REFERENCES Motos(id_moto)
);


CREATE TABLE CategoriasAccesorios (
  id_categoria INT AUTO_INCREMENT PRIMARY KEY,
  nombre_categoria VARCHAR(255) NOT NULL UNIQUE,
  descripcion TEXT
);

CREATE TABLE Accesorios (
  id_accesorio INT AUTO_INCREMENT PRIMARY KEY,
  id_categoria INT,
  nombre_accesorio VARCHAR(255) NOT NULL,
  descripcion TEXT,
  precio DECIMAL(10, 2),
  disponible BOOLEAN,
  creado_en TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (id_categoria) REFERENCES CategoriasAccesorios(id_categoria)
);

CREATE TABLE MotoAccesorios (
  id_moto INT,
  id_accesorio INT,
  PRIMARY KEY (id_moto, id_accesorio),
  FOREIGN KEY (id_moto) REFERENCES Motos(id_moto),
  FOREIGN KEY (id_accesorio) REFERENCES Accesorios(id_accesorio)
);