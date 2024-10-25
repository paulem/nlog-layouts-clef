// Seq Target for NLog - Copyright 2014-2017 Datalust and contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using NLog.Config;

namespace NLog.Layouts.ClefJsonLayout
{
    [Layout("ClefJsonLayout")]
    [Layout("CompactJsonLayout")]
    [ThreadAgnostic]
    public class CompactJsonLayout : JsonLayout
    {
        readonly JsonAttribute
            _timestampAttribute = new("@t", new SimpleLayout("${date:format=o}")),
            _levelAttribute = new("@l", new SimpleLayout("${level}")),
            _exceptionAttribute = new("@x", new SimpleLayout("${exception:format=toString}")),
            _messageAttribute = new("@m", new FormattedMessageLayout()),
            _messageTemplateAttribute = new("@mt", new SimpleLayout("${onhasproperties:${message:raw=true}}")),
            _traceIdAttribute = new("@tr", Layout.FromMethod(evt => System.Diagnostics.Activity.Current?.GetTraceId())),
            _spanIdAttribute = new("@sp", Layout.FromMethod(evt => System.Diagnostics.Activity.Current?.GetSpanId()));
        
        public Layout LogLevel { get => _levelAttribute.Layout; set => _levelAttribute.Layout = value; }

        public CompactJsonLayout()
        {
            Attributes.Add(_timestampAttribute);
            Attributes.Add(_levelAttribute);
            Attributes.Add(_exceptionAttribute);
            Attributes.Add(_messageTemplateAttribute);
            var renderingsAttribute = new JsonAttribute("@r", new RenderingsLayout(new Lazy<IJsonConverter>(ResolveService<IJsonConverter>)), encode: false);
            Attributes.Add(renderingsAttribute);
            Attributes.Add(_messageAttribute);
            Attributes.Add(_traceIdAttribute);
            Attributes.Add(_spanIdAttribute);
            
            IncludeEventProperties = true;
            IncludeScopeProperties = true;
            SuppressSpaces = true;
        }
    }
}