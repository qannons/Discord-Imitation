// Copyright 2015 MongoDB Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#pragma once

#include <bsoncxx/document/value.hpp>
#include <bsoncxx/document/view.hpp>
#include <bsoncxx/view_or_value.hpp>

#include <bsoncxx/config/prelude.hpp>

namespace bsoncxx {
namespace v_noabi {
namespace document {

using view_or_value = bsoncxx::v_noabi::view_or_value<view, value>;

}  // namespace document
}  // namespace v_noabi
}  // namespace bsoncxx

namespace bsoncxx {
namespace document {

using ::bsoncxx::v_noabi::document::view_or_value;

}  // namespace document
}  // namespace bsoncxx

#include <bsoncxx/config/postlude.hpp>
