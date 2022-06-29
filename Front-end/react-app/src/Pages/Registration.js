import React, { Component } from "react";
import { Variables } from "./Components/Variables";
import { useNavigate } from "react-router-dom";

class RegistrationClass extends Component {
  constructor(props) {
    super(props);
    this.state = {
      Nickname: "",
      Email: "",
      Password: "",
      ConfirmPassword: "",
    };

    this.changeNickname = this.changeNickname.bind(this);
    this.changeEmail = this.changeEmail.bind(this);
    this.changePassword = this.changePassword.bind(this);
    this.changeConfirmPassword = this.changeConfirmPassword.bind(this);
    this.createNewUser = this.createNewUser.bind(this);
  }

  changeNickname(event) {
    this.setState({ Nickname: event.target.value });
  }
  changeEmail(event) {
    this.setState({ Email: event.target.value });
  }
  changePassword(event) {
    this.setState({ Password: event.target.value });
  }
  changeConfirmPassword(event) {
    this.setState({ ConfirmPassword: event.target.value });
  }

  createNewUser() {
    if (
      this.state.Nickname == "" ||
      this.state.Email == "" ||
      this.state.Password == "" ||
      this.state.Password != this.state.ConfirmPassword
    ) {
      alert("Nickname or email or password is empty, please try again.");
      return;
    }

    let answerOk;
    fetch(Variables.API_URL + "/User", {
      method: "POST",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: this.state.Email,
        nickName: this.state.Nickname,
        password: this.state.Password,
        confirmPassword: this.state.ConfirmPassword,
      }),
    })
      .then((res) => {
        if (res.ok) {
          answerOk = true;
        } else if (res.status == 400) {
          answerOk = false;
          alert("Wrong pasword or Email , please try again.");
        } else {
          answerOk = false;
          alert("Something go wrong, please try again later.");
        }
      })
      .catch((error) => {
        alert("Something go wrong, please try again later.");
      })
      .then((result) => {
        if (answerOk) {
          alert("Congratulations you are registered, now you can login.");
          this.props.navigation("/");
          window.location.reload(false);
        }
      });
  }
  render() {
    const { Nickname, Email, Password, ConfirmPassword } = this.state;
    return (
      <div>
        <span>Nickname:</span>
        <input
          type="text"
          value={Nickname}
          onChange={this.changeNickname}
          placeholder="Your nickname"
        ></input>
        <span>Email:</span>
        <input
          value={Email}
          onChange={this.changeEmail}
          placeholder="Your email"
        ></input>
        <span>Password:</span>
        <input
          type="text"
          value={Password}
          onChange={this.changePassword}
          placeholder="Your password"
        ></input>
        <span>ConfirmPassword:</span>
        <input
          type="text"
          value={ConfirmPassword}
          onChange={this.changeConfirmPassword}
          placeholder="Your password"
        ></input>
        <button onClick={this.createNewUser}>CreateNewUser</button>
      </div>
    );
  }
}


export default function Login(props) {
  const navigation = useNavigate();

  return <RegistrationClass {...props} navigation={navigation} />;
}