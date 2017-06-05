/*
 * plants table 
 * This table will contain any plant name details that have a 1:1 mapping to plants
 * Other plant name details that have a 1:* mapping will need their own tables
 */
CREATE TABLE wetland.plants(
	PlantId serial NOT NULL,
    ID integer NOT NULL,
	SciNameAuthor text ,
	SciNameNoAuthor text ,
	Family text ,
	CommonName text ,
	PLANTSCode text,
	MapImg text ,
	ITISCode text ,
	AWWetCode text ,
	GPWetCode text ,
	WMVCWetCode text ,
	CValue text ,
	GRank text ,
	FederalStatus text ,
	COSRank text ,
	MTSRank text ,
	NDSRank text ,
	SDSRank text ,
	UTSRank text ,
	WYSRank text ,
	Nativity text ,
	NoxiousWeed text ,
	Duration text ,
	ElevMinFeet integer ,
	ElevMinM integer ,
	ElevMaxFeet integer ,
	ElevMaxM integer ,
	keychar1 text ,
	keychar2 text ,
	keychar3 text ,
	keychar4 text ,
	keychar5 text ,
	keychar6 text ,
	SimilarSp text ,
	Habitat text ,
	Comments text ,
	AnimalUse text ,
	EcologicalSystems text ,
	Synonyms text ,
	TopImgTopImg text ,
  CONSTRAINT PlantId_pk PRIMARY KEY (PlantId))
  WITH (OIDS=FALSE);

ALTER TABLE wetland.plants OWNER TO "PlantOwner";
GRANT SELECT ON wetland.plants TO "PlantUser";

/*
 * images table
 * 1:* connects to plants table
 */
CREATE TABLE wetland.images(
	ImageId serial NOT NULL,
	ID integer NOT NULL,
	PlantId integer NOT NULL,
FileName text,
Credit text,
  CONSTRAINT ImageId_pk PRIMARY KEY (ImageId),
  CONSTRAINT PlantId_fk FOREIGN KEY (PlantId) REFERENCES wetland.plants (PlantId) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE wetland.images OWNER TO "PlantOwner";
GRANT SELECT ON wetland.images TO "PlantUser";

/*
 * Similar Species table
 * 1:* connects to plants table
 */
CREATE TABLE wetland.similar_species(
	SimilarSpeciesId serial NOT NULL,
	ID integer NOT NULL,
	PlantId integer NOT NULL,
SimilarSpIcon text,
SimilarSpSciNameAuthor text,
Reason text,
  CONSTRAINT SimilarSpeciesId_pk PRIMARY KEY (SimilarSpeciesId),
  CONSTRAINT PlantId_fk FOREIGN KEY (PlantId) REFERENCES wetland.plants (PlantId) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE wetland.similar_species OWNER TO "PlantOwner";
GRANT SELECT ON wetland.similar_species TO "PlantUser";

/*
 * County Plant table.  This table is just an exhaustive association between plants and county.  Counties can repeat
 * 1:* connects to plants table
 */
CREATE TABLE wetland.county_plant(
	CountyPlantId serial NOT NULL,
	County_Id integer NOT NULL,
	PlantId integer NOT NULL,
Name text NOT NULL,
  CONSTRAINT CountyPlantId_pk PRIMARY KEY (CountyPlantId),
   CONSTRAINT PlantId_fk FOREIGN KEY (PlantId) REFERENCES wetland.plants (PlantId) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE wetland.county_plant OWNER TO "PlantOwner";
GRANT SELECT ON wetland.county_plant TO "PlantUser";

/*
 * References table
 * 1:* connects to plants table
 */
CREATE TABLE wetland.references(
	ReferenceId serial NOT NULL,
	ID integer NOT NULL,
	PlantId integer NOT NULL,
Reference text,
FullCitation text,
  CONSTRAINT ReferenceId_pk PRIMARY KEY (ReferenceId),
  CONSTRAINT PlantId_fk FOREIGN KEY (PlantId) REFERENCES wetland.plants (PlantId) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE wetland.references OWNER TO "PlantOwner";
GRANT SELECT ON wetland.references TO "PlantUser";

/*
 * Fruits table
 * 1:* connects to plants table
 */
CREATE TABLE wetland.fruits(
	FruitId serial NOT NULL,
		PlantId integer NOT NULL,
	ValueID integer NOT NULL,
  CONSTRAINT FruitId_pk PRIMARY KEY (FruitId),
  CONSTRAINT PlantId_fk FOREIGN KEY (PlantId) REFERENCES wetland.plants (PlantId) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE wetland.fruits OWNER TO "PlantOwner";
GRANT SELECT ON wetland.fruits TO "PlantUser";

/*
 * Division table
 * 1:* connects to plants table
 */
CREATE TABLE wetland.division(
	DivisionId serial NOT NULL,
		PlantId integer NOT NULL,
	ValueID integer NOT NULL,
  CONSTRAINT DivisionId_pk PRIMARY KEY (DivisionId),
  CONSTRAINT PlantId_fk FOREIGN KEY (PlantId) REFERENCES wetland.plants (PlantId) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE wetland.division OWNER TO "PlantOwner";
GRANT SELECT ON wetland.division TO "PlantUser";


/*
 * Shape table
 * 1:* connects to plants table
 */
CREATE TABLE wetland.shape(
	ShapeId serial NOT NULL,
		PlantId integer NOT NULL,
	ValueID integer NOT NULL,
  CONSTRAINT ShapeId_pk PRIMARY KEY (ShapeId),
  CONSTRAINT PlantId_fk FOREIGN KEY (PlantId) REFERENCES wetland.plants (PlantId) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE wetland.shape OWNER TO "PlantOwner";
GRANT SELECT ON wetland.shape TO "PlantUser";


/*
 * Arrangement table
 * 1:* connects to plants table
 */
CREATE TABLE wetland.arrangement(
	ArrangementId serial NOT NULL,
		PlantId integer NOT NULL,
	ValueID integer NOT NULL,
  CONSTRAINT ArrangementId_pk PRIMARY KEY (ArrangementId),
  CONSTRAINT PlantId_fk FOREIGN KEY (PlantId) REFERENCES wetland.plants (PlantId) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE wetland.arrangement OWNER TO "PlantOwner";
GRANT SELECT ON wetland.arrangement TO "PlantUser";

/*
 * Size table
 * 1:* connects to plants table
 */
CREATE TABLE wetland.size(
	SizeId serial NOT NULL,
		PlantId integer NOT NULL,
	ValueID integer NOT NULL,
  CONSTRAINT SizeId_pk PRIMARY KEY (SizeId),
  CONSTRAINT PlantId_fk FOREIGN KEY (PlantId) REFERENCES wetland.plants (PlantId) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE wetland.size OWNER TO "PlantOwner";
GRANT SELECT ON wetland.size TO "PlantUser";

/*
 * Regions table
 * 1:* connects to plants table
 */
CREATE TABLE wetland.regions(
	RegionId serial NOT NULL,
		PlantId integer NOT NULL,
	ValueID integer NOT NULL,
  CONSTRAINT RegionId_pk PRIMARY KEY (RegionId),
  CONSTRAINT PlantId_fk FOREIGN KEY (PlantId) REFERENCES wetland.plants (PlantId) ON UPDATE CASCADE ON DELETE CASCADE)
  WITH (OIDS=FALSE);
ALTER TABLE wetland.regions OWNER TO "PlantOwner";
GRANT SELECT ON wetland.regions TO "PlantUser";

