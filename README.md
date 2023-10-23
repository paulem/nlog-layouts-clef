# NLog.Layouts.ClefJsonLayout

An NLog layout that writes JSON in Compact Log Event Format [CLEF](http://clef-json.org) that is compatible with [Seq](https://datalust.co/seq).

There are situations when it is not possible to send logs directly to Seq, for example, due to enterprise security policies. In this case, a Seq-compatible JSON log can be useful, which can be manually fed to Seq using [seqcli](https://github.com/datalust/seqcli) `ingest` command.

### Getting started

After installing NLog, install the `NLog.Layouts.ClefJsonLayout` package from NuGet:

```
dotnet add package NLog.Layouts.ClefJsonLayout
```

Then, use the `ClefJsonLayout` layout within the file target in your NLog configuration:

```xml
<nlog>
    <extensions>
        <add assembly="NLog.Layouts.ClefJsonLayout"/>
    </extensions>
    <targets>
        <target xsi:type="File" name="file" fileName="log.json">
            <layout xsi:type="ClefJsonLayout" />
        </target>
    </targets>
    <rules>
        <logger name="*" minlevel="Info" writeTo="file" />
    </rules>
</nlog>
```

### Attaching additional properties

The `target` declaration in `NLog.config` can be expanded with additional properties:

```xml
<target xsi:type="File" name="file" fileName="log.json">
    <layout xsi:type="ClefJsonLayout">
        <attribute name="logger" layout="${logger}"/>
        <attribute name="thread" layout="${threadid}"/>
    </layout>
</target>
```

Any properties specified here will be attached to all outgoing events. The value can be any supported [layout renderer](https://github.com/NLog/NLog/wiki/Layout-Renderers).

### Acknowledgements

The layout is the part of the [NLog.Targets.Seq](https://github.com/datalust/nlog-targets-seq) code that is responsible for generating JSON in [CLEF](http://clef-json.org).