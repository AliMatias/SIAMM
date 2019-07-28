
CREATE TABLE "moleculas_lista"(
	"id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"formula" TEXT NULL,
	"formula_nomenclatura_sistematica" TEXT NULL,
	"nomenclatura_stock" TEXT NULL,
	"nomenclatura_tradicional" TEXT NULL);


CREATE TABLE "moleculas_mapping_element"(
	"id_molecula" INTEGER,
	"id_elemento" INTEGER,
	"cantidad" INTEGER,
	
  	PRIMARY KEY (id_molecula, id_elemento, cantidad),

	CONSTRAINT fk_moleculas
    FOREIGN KEY (id_molecula)
    REFERENCES moleculas_lista(id),

	CONSTRAINT fk_elementos
    FOREIGN KEY (id_elemento)
    REFERENCES valida_elementos(id));

CREATE TABLE "moleculas_posicion3D_element"(
	"id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"id_molecula" INTEGER,
	"id_elemento" INTEGER,
	"posX" INTEGER,
	"posY" INTEGER,
	"posZ" INTEGER,
	"escala" INTEGER,
	"conexion" INTEGER NULL,
	"tipo_conexion" INTEGER NULL
	"tipo_linea" INTEGER NULL);

