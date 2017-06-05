-- Role: PlantUser

-- DROP ROLE "PlantUser";

CREATE ROLE "PlantUser" LOGIN
  ENCRYPTED PASSWORD 'md58aef147ba7993b6de8ae1db942c407b3'
  NOSUPERUSER INHERIT NOCREATEDB NOCREATEROLE NOREPLICATION;


CREATE ROLE "PlantOwner" LOGIN
  ENCRYPTED PASSWORD 'md5afd087ef71117373c96bc71931aaf215'
  NOSUPERUSER INHERIT CREATEDB NOCREATEROLE REPLICATION;

CREATE DATABASE plant
  WITH OWNER = "PlantOwner"
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       LC_COLLATE = 'English_United States.1252'
       LC_CTYPE = 'English_United States.1252'
       CONNECTION LIMIT = -1;

/*
 * plants table 
 * This table will contain any plant name details that have a 1:1 mapping to plants
 * Other plant name details that have a 1:* mapping will need their own tables
 */
CREATE TABLE plant.plants(
	plant_id serial NOT NULL,
    plant_imported_id integer NOT NULL,
	common_name text NOT NULL,
	common_family_name text NOT NULL,
	scientific_family_name text NOT NULL,
	family_name_meaning text NOT NULL,
	family_characteristics text,
	classification text NOT NULL,
	sub_class text NOT NULL,
  CONSTRAINT plant_id_pk PRIMARY KEY (plant_id))
  WITH (OIDS=FALSE);

ALTER TABLE plant.plants OWNER TO "PlantOwner";
GRANT SELECT ON plant.plants TO "PlantUser";

/*
 * other_common_names table
 * 1:* connects to plants table
 */
CREATE TABLE plant.other_common_names(
	other_common_name_id serial NOT NULL,
	plant_id integer NOT NULL,
other_common_name text NOT NULL,
  CONSTRAINT other_common_name_id_pk PRIMARY KEY (other_common_name_id),
  CONSTRAINT plant_id_fk FOREIGN KEY (plant_id) REFERENCES plant.plants (plant_id) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE plant.other_common_names OWNER TO "PlantOwner";
GRANT SELECT ON plant.other_common_names TO "PlantUser";

/*
 * scientific_name table
 * 1:* connects to plants table
 */
CREATE TABLE plant.scientific_name(
	scientific_name_id serial NOT NULL,
	plant_id integer NOT NULL,
subspecies text,
variety text,
authors text,
scientific_name_meaning text,
  CONSTRAINT scientific_name_id_pk PRIMARY KEY (scientific_name_id),
  CONSTRAINT plant_id_fk FOREIGN KEY (plant_id) REFERENCES plant.plants (plant_id) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE plant.scientific_name OWNER TO "PlantOwner";
GRANT SELECT ON plant.scientific_name TO "PlantUser";

/*
 * synonyms table
 * 1:* connects to plants table
 */
CREATE TABLE plant.synonyms(
	synonym_id serial NOT NULL,
	plant_id integer NOT NULL,
synonym text NOT NULL,
  CONSTRAINT synonym_id_pk PRIMARY KEY (synonym_id),
  CONSTRAINT plant_id_fk FOREIGN KEY (plant_id) REFERENCES plant.plants (plant_id) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE plant.synonyms OWNER TO "PlantOwner";
GRANT SELECT ON plant.synonyms TO "PlantUser";

/*
 * Identification table
 * 1:* connects to plants table
 */
CREATE TABLE plant.identifications(
	identification_id serial NOT NULL,
	plant_id integer NOT NULL,
key_characteristics text NOT NULL,
mature_height text NOT NULL,
mature_spread text NOT NULL,
flower_cluster text NOT NULL,
flower_color text NOT NULL,
flower_shape text NOT NULL,
flower_size text NOT NULL,
flower_structure text NOT NULL,
flower_symmetry text NOT NULL,
fruit_color text NOT NULL,
fruit_type text NOT NULL,
leaf_type text NOT NULL,
leaf_shape text NOT NULL,
  CONSTRAINT identification_id_pk PRIMARY KEY (identification_id),
  CONSTRAINT plant_id_fk FOREIGN KEY (plant_id) REFERENCES plant.plants (plant_id) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE plant.identifications OWNER TO "PlantOwner";
GRANT SELECT ON plant.identifications TO "PlantUser";

/*
 * Ecology table
 * 1:* connects to plants table
 */
CREATE TABLE plant.ecologies(
	ecology_id serial NOT NULL,
	plant_id integer NOT NULL,
origin text NOT NULL,
conservation_status text NOT NULL,
life_zone text NOT NULL,
ecosystem_type text NOT NULL,
habitat text NOT NULL,
indicator_status text NOT NULL,
endemic_location text NOT NULL,
growth_form text NOT NULL,
life_cycle text NOT NULL,
monoecious text NOT NULL,
season_of_bloom text NOT NULL,
eco_relationships text NOT NULL,
wildlife_use text NOT NULL,
weed_management text NOT NULL,
  CONSTRAINT ecology_id_pk PRIMARY KEY (ecology_id),
  CONSTRAINT plant_id_fk FOREIGN KEY (plant_id) REFERENCES plant.plants (plant_id) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE plant.ecologies OWNER TO "PlantOwner";
GRANT SELECT ON plant.ecologies TO "PlantUser";


/*
 * Landscaping table
 * 1:* connects to plants table
 */
CREATE TABLE plant.landscapings(
	landscaping_id serial NOT NULL,
	plant_id integer NOT NULL,
landscaping_use text NOT NULL,
moisture_requirement text NOT NULL,
light_requirement text NOT NULL,
soil_requirement text NOT NULL,
seasonal_interest text NOT NULL,
cultivars text NOT NULL,
availability text NOT NULL,
  CONSTRAINT landscaping_id_pk PRIMARY KEY (landscaping_id),
  CONSTRAINT plant_id_fk FOREIGN KEY (plant_id) REFERENCES plant.plants (plant_id) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE plant.landscapings OWNER TO "PlantOwner";
GRANT SELECT ON plant.landscapings TO "PlantUser";

/*
 * Human Connections table
 * 1:* connects to plants table
 */
CREATE TABLE plant.human_connections(
	human_connection_id serial NOT NULL,
	plant_id integer NOT NULL,
livestock_uses text NOT NULL,
fiber text NOT NULL,
other text NOT NULL,
  CONSTRAINT human_connection_id_pk PRIMARY KEY (human_connection_id),
  CONSTRAINT plant_id_fk FOREIGN KEY (plant_id) REFERENCES plant.plants (plant_id) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE plant.human_connections OWNER TO "PlantOwner";
GRANT SELECT ON plant.human_connections TO "PlantUser";

/*
 * images table
 * 1:* connects to plants table
 */
CREATE TABLE plant.images(
	image_id serial NOT NULL,
	plant_id integer NOT NULL,
lo_res_path text NOT NULL,
high_res_path text NOT NULL,
  CONSTRAINT image_id_pk PRIMARY KEY (image_id),
  CONSTRAINT plant_id_fk FOREIGN KEY (plant_id) REFERENCES plant.plants (plant_id) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE plant.images OWNER TO "PlantOwner";
GRANT SELECT ON plant.images TO "PlantUser";


/*
 * locations table
 * 1:* connects to plants table
 */
CREATE TABLE plant.locations(
	location_id serial NOT NULL,
	plant_id integer NOT NULL,
state text NOT NULL,
county text NOT NULL,
  CONSTRAINT location_id_pk PRIMARY KEY (location_id),
  CONSTRAINT plant_id_fk FOREIGN KEY (plant_id) REFERENCES plant.plants (plant_id) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE plant.images OWNER TO "PlantOwner";
GRANT SELECT ON plant.images TO "PlantUser";
