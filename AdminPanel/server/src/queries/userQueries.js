// this file for admin sql queries ...
const getUserIdByToken = 'select "UserId" from "AspNetUserTokens" where "Value" = $1';
const getRoleName = 'select "Name" from "AspNetRoles" r join "AspNetUserRoles" ur on r."Id" =ur."RoleId" where ur."RoleId" = $1';
const findUser = 'select "Id","Avatar","UserName","NormalizedUserName","Email","NormalizedEmail","EmailConfirmed","PasswordHash" from "AspNetUsers" where "Email" = $1';
const addUser = 'insert into "AspNetUsers" ("Id","Email","PasswordHash","EmailConfirmed") values ($1,$2,$3,true);';
const saveToken = 'insert into "AspNetUserTokens" ("UserId", "LoginProvider", "Name", "Value") values ($1,\'login\',\'accessToken\',$2)';
module.exports = {
  getUserIdByToken,
  getRoleName,
  findUser,
  addUser,
  saveToken
};
