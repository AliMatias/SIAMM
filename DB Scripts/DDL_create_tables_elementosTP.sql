
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
	"resumen" TEXT NULL);

CREATE TABLE "elementos_info_detalle"(
	"id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"numero_atomico" INTEGER NULL,
	"isotopos_estables" TEXT NULL,
	"isotopos_aplicaciones"	TEXT NULL,
	"tipo_electrico" TEXT NULL,
	"radiactivo" TEXT NULL,
	"abundancia_corteza_terrestre" TEXT NULL,
	"descubrimiento" TEXT NULL,
	"descubierto_por" TEXT NULL,
	"angulos_de_red" TEXT NULL,
	"vida_media" TEXT NULL,
	"modulo_compresibilidad" TEXT NULL,
	"dureza_brinell" TEXT NULL,
	"presion_critica" TEXT NULL,
	"temperatura_critica" TEXT NULL,
	"conductividad_electrica" TEXT NULL,
	"densidad" REAL NULL,
	"radio_covalente" TEXT NULL,
	"afinidad_electronica" TEXT NULL,
	"punto_curie" TEXT NULL,
	"modo_decaimiento" TEXT NULL,
	"electronegatividad" REAL NULL,
	"densidadliquida" TEXT NULL,
	"constante_red" TEXT NULL,
	"multiplicidad_atomica_gas" TEXT NULL,
	"calor_de_fusion" TEXT NULL,
	"calor_de_vaporizacion" TEXT NULL,
	"tipo_magnetico" TEXT NULL,
	"susceptibilidad_magnetica" TEXT NULL,
	"volumen_molar" REAL NULL,
	"radio_poisson" TEXT NULL,
	"numeros_cuanticos" TEXT NULL,
	"indice_refractivo" REAL NULL,
	"resistividad" TEXT NULL,
	"conductividad_termica" TEXT NULL,
	"punto_superconductividad" TEXT NULL,
	"expansion_termica" TEXT NULL,
	"velocidad_sonido" TEXT NULL,
	"numero_grupos_espaciales" REAL NULL,
	"nombre_grupo_espacial" TEXT NULL,
	"radio_van_der_waals" TEXT NULL,
	"radio_atomico_en_angstroms" REAL NULL,
	"radio_covalente_en_angstroms" REAL NULL,
	"radio_van_der_waals_en_angstroms" REAL NULL,
	"modulo_young" TEXT NULL,
	"nombres_alotropicos" TEXT NULL,
	"energias_de_ionizacion" TEXT NULL);


CREATE TABLE "isotopos"(
	"id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"numero_atomico" INTEGER NULL,
	"numero_correlativo" INTEGER NULL,
	"isotopo" INTEGER NULL,
	"numero_masa" INTEGER NULL,
	"masa_atomica_relativa" TEXT NULL,
	"composicion_isotopica" TEXT NULL,
	"peso_atomico_estandar" TEXT NULL); 

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


			
