shader_type spatial;
render_mode vertex_lighting, fog_disabled;

uniform bool enable = true;
uniform sampler2D tex: source_color;
uniform sampler2D normal_map: source_color;
uniform float normal_depth = 1.0;

varying flat vec3 flat_vertex;
varying flat vec3 flat_normal;

void vertex() {
	flat_vertex = (MODELVIEW_MATRIX * vec4(VERTEX, 1.0)).xyz;
	flat_normal = NORMAL;
}

void fragment() {
	if (enable) {
		LIGHT_VERTEX = flat_vertex;
	}
	ALBEDO = texture(tex, UV).xyz;
	NORMAL_MAP = texture(normal_map, UV).xyz;
	NORMAL_MAP_DEPTH = normal_depth;
}