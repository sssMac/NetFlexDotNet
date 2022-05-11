// this file for sql queries ...
const push = 'insert into "Reviews" ("Id", "UserName", "ContentId", "Text", "Rating", "PublishTime") values ($1,$2,$3,$4,$5,$6)';
const remove = 'delete from "Reviews" where "Id" = $1;';
const getAll = 'select * from "Reviews"';
const getById = 'select * from "Reviews" where "Id" = $1';
const getByContentID = 'select * from "Reviews" where "ContentId" = $1';

module.exports = {
    push,
    getAll,
    getById,
    remove,
    getByContentID
};
