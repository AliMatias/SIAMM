
CREATE TABLE "materiales_lista"(
	"id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"nombre" TEXT NULL);


CREATE TABLE "materiales_mapping_element"(
	"id_material" INTEGER,
	"id_elemento" INTEGER NULL,
	"id_molecula" INTEGER NULL,
	"cantidad" INTEGER,
	
  	PRIMARY KEY (id_material, id_molecula, id_elemento, cantidad),

	CONSTRAINT fk_material
    FOREIGN KEY (id_material)
    REFERENCES materiales_lista(id),
	
	CONSTRAINT fk_moleculas
    FOREIGN KEY (id_molecula)
    REFERENCES moleculas_lista(id),

	CONSTRAINT fk_elementos
    FOREIGN KEY (id_elemento)
    REFERENCES valida_elementos(id));


