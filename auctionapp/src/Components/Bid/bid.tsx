import * as React from 'react';
import "./bid.css";
import { config } from "../../ApiConfig"
import { UserProps } from "../../Common/types"
import { HubConnection } from '@aspnet/signalr';

type ProductProps = {
    productId: number;
    actualBid?: number;
    hubConnetion: HubConnection;
} & UserProps;

export default class Bid extends React.PureComponent<ProductProps, {}> {

    state = {
        newBid: 0
    }


    sendMessage = () => {
        this.props.hubConnetion
            .invoke("SendToGroup", this.props.userName, this.state.newBid, this.props.productId.toString())
            .catch(err => console.error(err));

        this.setState({ message: '' });
    };

    validateForm = () => {
        return (this.state.newBid > 0 && (!this.props.actualBid || this.state.newBid > this.props.actualBid));
    }

    submitForm = () => {
        let params = {
            "userName": this.props.userName,
            "maxBid": this.state.newBid,
        };
        let headers = {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
        let apiLocation = config.productsApi + "product/" + this.props.productId + "/bid/";
        fetch(apiLocation, {
            method: "POST",
            headers: headers,
            body: JSON.stringify(params)
        }).then((res) => {
            this.sendMessage();
        })
            .then((data) => {
                console.log(data);
                if (data != null) {
                    console.log(data);
                    this.sendMessage();
                }
            })
            .catch((err) => {
                console.log(err);
            });
    }

    handleChange = (value: string) => {
        this.setState({ newBid: value })
    }

    private handleSubmit = async (
        e: React.FormEvent<HTMLFormElement>
    ): Promise<void> => {
        e.preventDefault();

        if (this.validateForm()) {
            await this.submitForm();
        }
    };

    render() {

        return (
            <div className="product-bid">
                <form onSubmit={this.handleSubmit}>
                    <label>
                        Offer:
                    <input type="text" pattern="[0-9]*" value={this.state.newBid} onChange={(e) => this.handleChange(e.target.value)} />
                    </label>
                    <div className="form-group">
                        <button type="submit" className="btn btn-primary">
                            Offer
                        </button>
                    </div>
                </form>
            </div>
        )
    }
}