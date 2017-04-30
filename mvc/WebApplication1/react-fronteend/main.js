import React from 'react'
import ReactDOM from 'react-dom'
import uploadApp from './a'
import Rx from 'rx-dom'

//TODO: hook up event stream to /messages
var element = (
  <div>something</div>
);

console.log(Rx);
var source = Rx.DOM.fromEventSource('/messages');
source.subscribe((e) => console.log(e), (error) => console.log(error), () => console.log('completed'));

console.log('subscribed')
//var source = new EventSource('/messages');
var mountNode = document.getElementById('app');

//source.addEventListener('open', function(e) {
//	console.log('open');
//	console.log(e);
//}, false);

//source.addEventListener('message', function(e) {
//	console.log(e.data);
//  var node = document.createTextNode(e.data);
//  var br = document.createElement('br');
//  mountNode.appendChild(node);
//  mountNode.appendChild(br);
//}, false);

console.log(mountNode);
ReactDOM.render(element, mountNode);
