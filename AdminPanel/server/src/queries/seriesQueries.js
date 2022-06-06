// this file for admin sql queries ...
const addSerial = 'insert into "Serials" ("Id", "Poster", "Title", "NumEpisodes", "AgeRating", "UserRating", "Description") values ($1,$2,$3,$4,$5,$6,$7)';
const deleteSerial = 'delete from "Serials" where "Id" = $1'
const renameSerial = 'update "Serials" set "SerialName" = $1 where "Id" = $2';
const findSerial = 'select * from "Serials" where "SerialName" = $1';
const findSerialById = 'select * from "Serials" where "Id" = $1';
const getAllSerials = 'select * from "Serials"';


module.exports = {
  addSerial,
  deleteSerial,
  renameSerial,
  findSerial,
  findSerialById,
  getAllSerials
};
