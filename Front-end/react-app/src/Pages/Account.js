import { Component } from "react";
import { Variables } from "./Components/Variables";
import { useNavigate } from "react-router-dom";

class AccountClass extends Component {
  constructor(props) {
    super(props);
    this.state = {
      nickname: "",
      email: "",
      password: "",
      registrationDate: "",
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
      .then((res) => {
        if (res.status === 200) {
          return;
        } else if (res.status === 401) {
          alert("You was unauthorized please login again.");
          this.props.navigation("/login");
          window.location.reload(false);
          return;
        } else if (res.status === 409) {
          alert("This nickname already exist, please try another.");
          return;
        } else if (res.status === 400) {
          alert("Wrong input");
          return;
        } else {
          alert("Something go wrong, please try again later.");
          return;
        }
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
      .then((res) => {
        if (res.status === 200) {
          return;
        } else if (res.status === 401) {
          alert("You was unauthorized please login again.");
          this.props.navigation("/login");
          window.location.reload(false);
          return;
        } else if (res.status === 409) {
          alert("This email already used, please try another.");
          return;
        } else if (res.status === 400) {
          alert("Wrong input");
          return;
        } else {
          alert("Something go wrong, please try again later.");
          return;
        }
      })
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
      .then((res) => {
        if (res.status === 200) {
          return;
        } else if (res.status === 401) {
          alert("You was unauthorized please login again.");
          this.props.navigation("/login");
          window.location.reload(false);
          return;
        } else if (res.status === 400) {
          alert("Wrong input");
          return;
        } else {
          alert("Something go wrong, please try again later.");
          return;
        }
      })
      .then((res) => res)
      .then((result) => {
        this.refreshUser();
      });
  }

  refreshUser() {
    let answerOk = false;
    fetch(Variables.API_URL + "/User", {
      method: "GET",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("access_token"),
      },
    })
      .then((res) => {
        if (res.status === 200) {
          answerOk = true;
          return res.json();
        } else if (res.status === 401) {
          alert("You was unauthorized please login again.");
          this.props.navigation("/login");
          window.location.reload(false);
          return;
        } else {
          alert("Something go wrong, please try again later.");
          return;
        }
      })
      .then((result) => {
        if (!answerOk) {
          return;
        }
        this.setState({
          nickname: result.nickName,
          email: result.email,
          password: result.password,
          registrationDate: result.registrationDate,
        });
      });
  }

  componentDidMount() {
    this.refreshUser();
  }

  render() {
    const {
      nickname,
      email,
      password,
      registrationDate,
      Nnickname,
      Nemail,
      Npassword,
    } = this.state;

    let CheckEmail = sessionStorage.getItem("Email");

    if (CheckEmail == null) {
      alert("You was unauthorized please login again.");
      this.props.navigation("/login");
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
            <div className="inputDiv">
              <p>User registrationDate : {registrationDate.slice(0, 10)}</p>
            </div>
          </div>
        </article>
      );
    }
  }
}

export default function Account(props) {
  const navigation = useNavigate();

  return <AccountClass {...props} navigation={navigation} />;
}
