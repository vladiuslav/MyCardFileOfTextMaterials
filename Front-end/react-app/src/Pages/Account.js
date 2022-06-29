import { Component } from "react";
import { Variables } from "./Components/Variables";
export default class Account extends Component {
  constructor(props) {
    super(props);
    this.state = {
      nickname: "",
      email: "",
      password: "",
      Nnickname: "",
      Nemail: "",
      Npassword: "",
    };
    this.changeNickname = this.changeNickname.bind(this);
    this.changeEmail = this.changeEmail.bind(this);
    this.changePassword = this.changePassword.bind(this);
    this.fetchUserNickname = this.fetchUserNickname.bind(this);
    this.fetchUserEmail = this.fetchUserEmail.bind(this);
    this.fetchUserPassword = this.fetchUserPassword.bind(this);
  }

  changeNickname(event) {
    this.setState({ Nnickname: event.target.value });
  }

  changeEmail(event) {
    this.setState({ Nemail: event.target.value });
  }
  changePassword(event) {
    this.setState({ Npassword: event.target.value });
  }

  fetchUserNickname() {
    fetch(Variables.API_URL + "/User/changeNickname", {
      method: "PUT",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
      body: JSON.stringify(this.state.Nnickname),
    })
      .then(result => {
        this.refreshUser();
      });
  }
  fetchUserEmail() {
    fetch(Variables.API_URL + "/User/changeEmail", {
      method: "PUT",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
      body: this.state.Nemail,
    })
      .then((res) => res)
      .then((result) => {
        this.refreshUser();
      });
  }
  fetchUserPassword() {
    fetch(Variables.API_URL + "/User/changePassword", {
      method: "PUT",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
      body: this.state.Npassword,
    })
      .then((res) => res)
      .then((result) => {
        this.refreshUser();
      });
  }

  refreshUser() {
    fetch(Variables.API_URL + "/User", {
      method: "GET",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
    })
      .then((res) => res.json())
      .then((result) => {
        this.setState({
          nickname: result.nickName,
          email: result.email,
          password: result.password,
        });
      });
  }

  componentDidMount() {
    this.refreshUser();
  }

  render() {
    const { nickname, email, password, Nnickname, Nemail, Npassword } =
      this.state;

    let CheckEmail = sessionStorage.getItem("Email");

    if (CheckEmail == null) {
      return <div>You are not logged, please login</div>;
    } else {
      return (
        <article>
          <p>User nickname : {nickname} </p>
          <input
            type="text"
            value={Nnickname}
            onChange={this.changeNickname}
            placeholder="Your new nickname"
          />
          <button onClick={this.fetchUserNickname}>Change nickname</button>
          <p>User email : {email}</p>{" "}
          <input
            type="text"
            value={Nemail}
            onChange={this.changeEmail}
            placeholder="Your new email"
          />
          <button onClick={this.fetchUserEmail}>Change email</button>
          <p>User password : {password}</p>{" "}
          <input
            type="text"
            value={Npassword}
            onChange={this.changePassword}
            placeholder="Your new password"
          />
          <button onClick={this.fetchUserPassword}>Change password</button>
        </article>
      );
    }
  }
}
