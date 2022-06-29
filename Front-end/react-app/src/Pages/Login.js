import React, { Component } from "react";
import { Variables } from "./Components/Variables";
import { useNavigate } from "react-router-dom";

class LoginClass extends Component {
  constructor(props) {
    super(props);
    this.state = {
      Email: "",
      Password: "",
    };

    this.changeEmail = this.changeEmail.bind(this);
    this.changePassword = this.changePassword.bind(this);
    this.Login = this.Login.bind(this);
  }
  changeEmail(event) {
    this.setState({ Email: event.target.value });
  }
  changePassword(event) {
    this.setState({ Password: event.target.value });
  }
  Login() {
    if (this.state.Email === "" || this.state.Password === "") {
      alert("Email or passwor is empty, please try again.");
      return;
    }

    let answerOk;
    Promise.all([
      fetch(Variables.API_URL + "/User/login", {
        method: "POST",
        headers: {
          accept: "application/json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: this.state.Email,
          password: this.state.Password,
        }),
      })
        .then((res) => {
          if (res.ok) {
            answerOk = true;
            return res.json();
          } else if (res.status == 400) {
            answerOk = false;
            alert("Wrong pasword or email , please try again.");
          } else {
            answerOk = false;
            alert("Something go wrong, please try again later.");
          }
        })
        .catch((error) => {
          alert("Something go wrong, please try again later.");
        }),
    ]).then((value) => {
      if (answerOk) {
        console.log(value[0]);

        sessionStorage.setItem("access_token", value[0].access_token);
        sessionStorage.setItem("Email", value[0].email);
        alert("You are logged");
        this.props.navigation("/");
        window.location.reload(false);
      }
    });
  }

  render() {
    const { Email, Password } = this.state;
    return (
      <div className="login">
        <span>Email: </span>
        <input
          value={Email}
          onChange={this.changeEmail}
          placeholder="Your email"
        ></input>
        <span>Password: </span>
        <input
          type="text"
          value={Password}
          onChange={this.changePassword}
          placeholder="Your password"
        ></input>
        <button onClick={this.Login}>Login</button>
      </div>
    );
  }
}

export default function Login(props) {
  const navigation = useNavigate();

  return <LoginClass {...props} navigation={navigation} />;
}