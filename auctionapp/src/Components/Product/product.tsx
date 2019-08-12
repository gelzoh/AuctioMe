import * as React from 'react';
import "./product.css";
import Moment from 'react-moment';
import { UserProps } from "../../Common/types"
import Bid from "../Bid/bid"
import { HubConnection } from '@aspnet/signalr';

type BidProps = {
    userName: string,
    maxBid: number
}

type ProductProps = {
    data: {
        id: number;
        name: string;
        expirationDate: Date;
        image?: string;
        lastBid: BidProps
    };
    hubConnection: HubConnection;
} & UserProps;

export default class Product extends React.Component<ProductProps, {}> {


    state = {
        bid: 0,
        userName: "",
        counter: ""
    }

    registerProductUpdate() {

        this.props.hubConnection.on(this.props.data.id.toString(), (winner, bid) => {
            this.setState({ bid: bid, userName: winner });
        });
    }

    componentDidMount() {
        this.registerProductUpdate();
        if (this.props.data.lastBid) {
            this.setState({ userName: this.props.data.lastBid.userName, bid: this.props.data.lastBid.maxBid })
        }
        this.countdown(new Date(this.props.data.expirationDate).getTime());
    }

    countdown(date: number) {
        let x = setInterval(() => {
            let dateDif = date - new Date().getTime();
            let days = Math.floor(dateDif / (1000 * 60 * 60 * 24));
            let hours = Math.floor((dateDif % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            let minutes = Math.floor((dateDif % (1000 * 60 * 60)) / (1000 * 60));
            let seconds = Math.floor((dateDif % (1000 * 60)) / 1000);

            let result = days + " days " + hours + " hours " + minutes + " minutes " + seconds + " seconds";
            this.setState({ counter: result });

            if (dateDif <= 0) {
                clearInterval(x);
            }
        }, 1000);
    }

    getWinner(winner: string, bid: number) {
        if (bid > 0) {
            return <p>The current winner is {winner} with a bid of {bid}</p>
        }
        else {
            return <p>There are no bids yet.</p>
        }
    }

    render() {
        const dateDif = new Date(this.props.data.expirationDate).getTime() - new Date().getTime();
        return (
            <div className="product">
                <img src={this.props.data.image} alt="" title="" />
                {(new Date().getTime() < new Date(this.props.data.expirationDate).getTime()) ?
                    <div className="product-content">
                        <h2>{this.props.data.name}</h2>
                        {this.getWinner(this.state.userName, this.state.bid)}
                        <span>The aution ends in:</span>
                        <span className="counter">{this.state.counter}</span>
                        <Bid productId={this.props.data.id} actualBid={this.state.bid} hubConnetion={this.props.hubConnection} userName={this.props.userName} />
                    </div> :
                    <div>
                        <h3>{this.props.data.name}</h3>
                        {this.getWinner(this.state.userName, this.state.bid)}
                        <span>The aution finished at <Moment date={this.props.data.expirationDate} format="DD-MM-YYYY hh:mm" /></span>
                    </div>
                }
            </div>
        )
    }
}