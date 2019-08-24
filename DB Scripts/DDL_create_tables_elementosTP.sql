CREATE TABLE "elementos_info_basica"(
	"id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"numero_atomico" INTEGER NULL,
	"simbolo" TEXT NULL,
	"nombre" TEXT NULL,
	"peso_atomico" REAL NULL,
	"periodo" INTEGER NULL,
	"clasificacion"	TEXT NULL,
	"clasificacion_grupo" TEXT NULL,
	"estado_natural" TEXT NULL,
	"estructura_cristalina" TEXT NULL,
	"color" TEXT NULL,
	"valencia" TEXT NULL,
	"numeros_oxidacion" TEXT NULL,
	"configuracion_electronica_abreviada" TEXT NULL,
	"configuracion_electronica" TEXT NULL,
	"caracteristicas" TEXT NULL,
	"punto_fusion" TEXT NULL,
	"punto_ebullicion" TEXT NULL,
	"resumen" TEXT NULL,
	

	CONSTRAINT fk_elementos
    FOREIGN KEY (numero_atomico)
    REFERENCES valida_elementos(id)
);


CREATE TABLE "elementos_info_detalle" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"numero_atomico"	INTEGER,
	"isotopos_estables"	TEXT,
	"isotopos_aplicaciones"	TEXT,
	"tipo_electrico"	TEXT,
	"radiactivo"	TEXT,
	"abundancia_corteza_terrestre"	TEXT,
	"descubrimiento"	TEXT,
	"descubierto_por"	TEXT,
	"angulos_de_red"	TEXT,
	"vida_media"	TEXT,
	"modulo_compresibilidad"	TEXT,
	"dureza_brinell"	TEXT,
	"presion_critica"	TEXT,
	"temperatura_critica"	TEXT,
	"conductividad_electrica"	TEXT,
	"densidad"	REAL,
	"radio_covalente"	TEXT,
	"afinidad_electronica"	TEXT,
	"punto_curie"	TEXT,
	"modo_decaimiento"	TEXT,
	"electronegatividad"	REAL,
	"densidadliquida"	TEXT,
	"constante_red"	TEXT,
	"multiplicidad_atomica_gas"	TEXT,
	"calor_de_fusion"	TEXT,
	"calor_de_vaporizacion"	TEXT,
	"tipo_magnetico"	TEXT,
	"susceptibilidad_magnetica"	TEXT,
	"volumen_molar"	REAL,
	"radio_poisson"	TEXT,
	"numeros_cuanticos"	TEXT,
	"indice_refractivo"	REAL,
	"resistividad"	TEXT,
	"conductividad_termica"	TEXT,
	"punto_superconductividad"	TEXT,
	"expansion_termica"	TEXT,
	"velocidad_sonido"	TEXT,
	"numero_grupos_espaciales"	REAL,
	"nombre_grupo_espacial"	TEXT,
	"radio_van_der_waals"	TEXT,
	"radio_atomico_en_angstroms"	REAL,
	"radio_covalente_en_angstroms"	REAL,
	"radio_van_der_waals_en_angstroms"	REAL,
	"modulo_young"	TEXT,
	"nombres_alotropicos"	TEXT,
	"energias_de_ionizacion"	TEXT,

	CONSTRAINT fk_elementos
    FOREIGN KEY (numero_atomico)
    REFERENCES valida_elementos(id)
);


CREATE TABLE "isotopos"(
	"id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"numero_atomico" INTEGER NULL,
	"numero_correlativo" INTEGER NULL,
	"isotopo" INTEGER NULL,
	"numero_masa" INTEGER NULL,
	"masa_atomica_relativa" TEXT NULL,
	"composicion_isotopica" TEXT NULL,
	"peso_atomico_estandar" TEXT NULL,
	
	CONSTRAINT fk_isotopos
    FOREIGN KEY (id)
    REFERENCES valida_isotopos(id),
	
	CONSTRAINT fk_elementos
    FOREIGN KEY (numero_atomico)
    REFERENCES valida_elementos(id)
);


CREATE TABLE "valida_elementos" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"numero"	INTEGER,
	"simbolo"	TEXT,
	"nombre"	TEXT,
	"electrones"	INTEGER,
	"protones"	INTEGER,
	"neutrones"	INTEGER,
	"maxelectronesgana"	INTEGER,
	"maxelectronespierde"	INTEGER
);


CREATE TABLE "valida_isotopos" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"numero_atomico"	INTEGER,
	"numero_correlativo"	INTEGER,
	"isotopo"	TEXT,
	"numero_de_masa"	INTEGER,
	"neutrones"	INTEGER,
	"estable"	INTEGER
);


CREATE TABLE "elementos_orbitas"(
	"id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"nro_orbita" INTEGER NULL,
	"nombre_capa" TEXT NULL,
	"max_electrones" INTEGER NULL);

CREATE TABLE sugerencias (
	id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	id_elemento INTEGER NOT NULL,
	id_sugerido INTEGER NOT NULL
);

			
