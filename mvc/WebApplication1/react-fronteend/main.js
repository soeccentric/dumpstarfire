import React from 'react'
import ReactDOM from 'react-dom'
import something from './a'

//TODO: hook up event stream to /messages
var element = (
  <div>something</div>
);
console.log(something.a());

var source = new EventSource('/messages');
var mountNode = document.getElementById('app');

source.addEventListener('message', function(e) {
  var node = document.createTextNode(e.data);
  var br = document.createElement('br');
  mountNode.appendChild(node);
  mountNode.appendChild(br);
}, false);

console.log(mountNode);
ReactDOM.render(element, mountNode);
