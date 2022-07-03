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
      .catch((error) => {
        alert("Something go wrong, please try again later.");
      })
      .then((result) => {
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
      .catch((error) => {
        alert("Something go wrong, please try again later.");
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
      .catch((error) => {
        alert("Something go wrong, please try again later.");
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
      .catch((error) => {
        alert("Something go wrong, please try again later.");
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
          <div className="loginForm">
            <div className="inputDiv">
              <p>User nickname : {nickname} </p>
              <input
                className="input"
                type="text"
                value={Nnickname}
                onChange={this.changeNickname}
                placeholder="Your new nickname"
              />
              <div className="buttonMerge">
                <button
                  className="simpleButton"
                  onClick={this.fetchUserNickname}
                >
                  Change nickname
                </button>
              </div>
            </div>
            <div className="inputDiv">
              <p>User email : {email}</p>{" "}
              <input
                className="input"
                type="text"
                value={Nemail}
                onChange={this.changeEmail}
                placeholder="Your new email"
              />
              <div className="buttonMerge"></div>
              <button className="simpleButton" onClick={this.fetchUserEmail}>
                Change email
              </button>
            </div>
            <div className="inputDiv">
              <p>User password : {password}</p>{" "}
              <input
                className="input"
                type="text"
                value={Npassword}
                onChange={this.changePassword}
                placeholder="Your new password"
              />
              <div className="buttonMerge">
                <button
                  className="simpleButton"
                  onClick={this.fetchUserPassword}
                >
                  Change password
                </button>
              </div>
            </div>
          </div>
        </article>
      );
    }
  }
}
