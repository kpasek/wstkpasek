import React, { Component } from "react";
import { Container } from "reactstrap";
import { NavMenu } from "./NavMenu";

export class Layout extends Component {
  static displayName = Layout.name;
  render() {
    return (
      <div>
        <NavMenu
          isAuthenticated={this.props.isAuthenticated}
          handleLogout={this.props.handleLogout}
        />
        <div className="container px-0 px-lg-2">{this.props.children}</div>
      </div>
    );
  }
}
