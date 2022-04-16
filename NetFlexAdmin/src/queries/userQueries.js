// this file for admin sql queries ...
const getUserIdByToken = 'select "UserId" from "AspNetUserTokens" where "Value" = $1';
const getRoleName = 'select "Name" from "AspNetRoles" r join "AspNetUserRoles" ur on r."Id" =ur."RoleId" where ur."RoleId" = $1';

module.exports = {
  getUserIdByToken,
  getRoleName
};
