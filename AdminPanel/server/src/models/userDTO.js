class UserDTO {
    constructor(Id, Email,EmailConfirmed,Status ) {
        this.Id = Id;
        this.Email = Email;
        this.EmailConfirmed = EmailConfirmed;
        this.Status = Status;
    }
}

module.exports = UserDTO;
