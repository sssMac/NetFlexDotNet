class UserDTO {
    constructor(Id, Email,UserName,Avatar, EmailConfirmed,Status ) {
        this.Id = Id;
        this.Email = Email;
        this.UserName = UserName;
        this.Avatar = Avatar;
        this.EmailConfirmed = EmailConfirmed;
        this.Status = Status;
    }
}

module.exports = UserDTO;
