import React, { Component } from "react";
import { NavMenu } from "./NavMenu";

export class Layout extends Component {
  static displayName = Layout.name;
  render() {
    return (
      <React.Fragment>
        <div>
          <NavMenu
            isAuthenticated={this.props.isAuthenticated}
            handleLogout={this.props.handleLogout}
          />
          <div className="container">
            <div className="row">{this.props.children}</div>
          </div>
        </div>
      </React.Fragment>
    );
  }
}
