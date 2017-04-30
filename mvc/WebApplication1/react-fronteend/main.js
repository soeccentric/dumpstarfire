import React from 'react'
import ReactDOM from 'react-dom'
import something from './a'
import Rx from 'rx-dom'

var element = (
  <div>something</div>
);
console.log(something.a());

var observer = Rx.Observer.create(function (e) {
  console.log('Opening');
});

var source = Rx.DOM.fromEventSource('/messages', observer)
source.subscribe((e) => {
  var node = document.createTextNode(e);

});

var mountNode = document.getElementById('app');
source.subscribe((message) => {
  var node = document.createTextNode(message);
  var br = document.createElement('br');
  mountNode.appendChild(node);
  mountNode.appendChild(br);
});

//ReactDOM.render(element, mountNode);
